using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using SnekTech.GridCell;
using SnekTech.Player;
using SnekTech.Roguelike;
using SnekTech.UI;
using UnityEngine;

namespace SnekTech.Grid
{
    public class GridBehaviour : MonoBehaviour, IGrid
    {
        [SerializeField]
        private CellBehaviour cellBehaviour;

        [SerializeField]
        private CellSprites cellSprites;

        [SerializeField]
        private InputEventManager inputEventManager;

        [SerializeField]
        private UIEventManager uiEventManager;

        [SerializeField]
        private GridEventManager gridEventManager;

        [SerializeField]
        private PlayerState playerState;

        [SerializeField]
        private UIState uiState;

        [SerializeField]
        private Camera mainCamera;
        
        private int _cellLayer;

        private IGridBrain _gridBrain;


        private List<Sprite> NoBombSprites => cellSprites.noBombSprites;
        private Sprite BombSprite => cellSprites.bombSprite;

        public Dictionary<ICell, GridIndex> CellIndexDict { get; } = new Dictionary<ICell, GridIndex>();
        public List<ICell> Cells { get; } = new List<ICell>();

        public GridData GridData { get; private set; }

        private GridSize GridSize => GridData.GridSize;
        public int CellCount => GridSize.rowCount * GridSize.columnCount;
        public int BombCount { get; private set; }

        public int RevealedCellCount => Cells.Count(cell => cell.IsRevealed);
        public int FlaggedCellCount => Cells.Count(cell => cell.IsFlagged);

        private bool IsAllCleared => Cells.Where(cell => !cell.HasBomb).All(cell => cell.IsRevealed);

        #region Unity callbacks
        
        private void Awake()
        {
            _gridBrain = new BasicGridBrain(this);

            _cellLayer = 1 << LayerMask.NameToLayer("Cell");
        }

        private void OnEnable()
        {
            inputEventManager.LeftClickPerformed += HandleOnLeftClick;
            inputEventManager.LeftDoubleClickPerformed += HandleOnLeftDoubleClick;
            inputEventManager.RightClickPerformed += HandleOnRightClick;
            inputEventManager.MovePerformed += OnMove;

            uiEventManager.ResetButtonClicked += OnResetButtonClicked;
        }

        private void OnDisable()
        {
            inputEventManager.LeftClickPerformed -= HandleOnLeftClick;
            inputEventManager.LeftDoubleClickPerformed -= HandleOnLeftDoubleClick;
            inputEventManager.RightClickPerformed -= HandleOnRightClick;
            inputEventManager.MovePerformed -= OnMove;

            uiEventManager.ResetButtonClicked -= OnResetButtonClicked;
        }
        
        #endregion

        #region Event Handlers
        
        private void HandleOnLeftClick(Vector2 mousePosition) => ProcessLeftClickAsync(mousePosition).Forget();

        private void HandleOnLeftDoubleClick(Vector2 mousePosition) =>
            ProcessLeftDoubleClickAsync(mousePosition).Forget();

        private void HandleOnRightClick(Vector2 mousePosition) => ProcessRightClickAsync(mousePosition).Forget();

        private void OnMove(Vector2 mousePosition)
        {
            var cellHovering = GetMouseHoveringCell(mousePosition);
            UpdateGridHighlight(cellHovering);
        }
        
        private void OnResetButtonClicked(GridData gridDataIn) => InitCells(gridDataIn);
        
        #endregion
        
        public async UniTaskVoid ProcessLeftClickAsync(Vector2 mousePosition)
        {
            var cell = GetMouseHoveringCell(mousePosition);
            bool canClickCell = cell is{IsCovered: true};
            if (!canClickCell)
            {
                return;
            }
            
            playerState.TriggerAllClickEffects();

            var affectedCells = _gridBrain.GetAffectedCellsWithinScope(cell, playerState.SweepScope);
            var revealCellTasks = Enumerable
                .Select(affectedCells, affectedCell => RevealCellAsync(CellIndexDict[affectedCell])).ToList();

            await UniTask.WhenAll(revealCellTasks);

            HandleRecursiveRevealCellComplete(cell);
        }

        public async UniTaskVoid ProcessLeftDoubleClickAsync(Vector2 mousePosition)
        {
            var cell = GetMouseHoveringCell(mousePosition);
            bool canDoubleClickCell = cell is {IsRevealed: true, HasBomb: false};
            if (!canDoubleClickCell)
            {
                return;
            }

            int userConfirmedBombCount = 0;
            int neighborFlagCount = _gridBrain.GetNeighborFlagCount(cell);
            _gridBrain.ForEachNeighbor(cell, neighborCell =>
            {
                if (neighborCell.HasBomb && neighborCell.IsRevealed)
                {
                    userConfirmedBombCount++;
                }
            });

            int actualNeighborBombCount = _gridBrain.GetNeighborBombCount(cell);
            if (userConfirmedBombCount + neighborFlagCount != actualNeighborBombCount)
            {
                return;
            }

            await RevealNeighbors(cell);
            
            HandleRecursiveRevealCellComplete(cell);
        }

        public async UniTaskVoid ProcessRightClickAsync(Vector2 mousePosition)
        {
            var cell = GetMouseHoveringCell(mousePosition);
            if (cell == null)
            {
                return;
            }

            bool isClickSuccessful = await cell.OnRightClick();

            if (isClickSuccessful)
            {
                gridEventManager.InvokeOnCellFlagOperated(this, cell);
            }
        }
        
        private async UniTask RevealCellAsync(GridIndex cellGridIndex)
        {
            if (!_gridBrain.IsIndexWithinGrid(cellGridIndex))
            {
                return;
            }

            var cell = _gridBrain.GetCellAt(cellGridIndex);
            if (!cell.IsCovered)
            {
                return;
            }

            // if last click action has not finished,
            // the task will return false,
            // to throttle the player input frequency
            bool isLeftClickSuccessful = await cell.OnLeftClick();
            if (!isLeftClickSuccessful)
            {
                return;
            }

            gridEventManager.InvokeOnCellReveal(this, cell);

            if (cell.HasBomb)
            {
                gridEventManager.InvokeOnBombReveal(this, cell);
                return;
            }

            if (_gridBrain.GetNeighborBombCount(cell) > 0)
            {
                return;
            }

            await RevealNeighbors(cell);
        }

        private UniTask RevealNeighbors(ICell cell)
        {
            var revealNeighborTasks = new List<UniTask>();
            _gridBrain.ForEachNeighbor(cell,
                neighborCell =>
                {
                    revealNeighborTasks.Add(RevealCellAsync(CellIndexDict[neighborCell]));
                });

            return UniTask.WhenAll(revealNeighborTasks);
        }

        private ICell GetMouseHoveringCell(Vector2 mousePosition)
        {
            if (uiState.isBlockingRaycast)
            {
                return null;
            }
            
            var ray = mainCamera.ScreenPointToRay(mousePosition);
            var hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, _cellLayer);

            return hit.collider != null ? hit.collider.GetComponent<ICell>() : null;
        }

        private void HandleRecursiveRevealCellComplete(ICell originalCell)
        {
            gridEventManager.InvokeOnRecursiveRevealComplete(originalCell);
            CheckIfAllCleared();
        }

        private void CheckIfAllCleared()
        {
            if (IsAllCleared)
            {
                gridEventManager.InvokeOnGridCleared(this);
            }
        }

        private void InitCells()
        {
            InstantiateCells(GridData);
            InitCellsContent();
            gridEventManager.InvokeOnGridInitComplete(this);
        }

        public void InitCells(GridData newGridData)
        {
            GridData = newGridData;
            InitCells();
        }

        private void DisposeCells()
        {
            foreach (var cell in Cells)
            {
                cell.Dispose();
            }

            Cells.Clear();
            CellIndexDict.Clear();
        }

        private void InstantiateCells(GridData newGridData)
        {
            DisposeCells();

            IRandomGenerator bombGenerator = RandomGenerator.Instance;
            BombCount = 0;

            var gridSize = newGridData.GridSize;
            for (int i = 0; i < gridSize.rowCount; i++)
            {
                for (int j = 0; j < gridSize.columnCount; j++)
                {
                    var cellMono = Instantiate(cellBehaviour, transform);
                    ICell cell = cellMono;
                    var cellIndex = new GridIndex(i, j);
                    cell.GridIndex = cellIndex;
                    cell.SetPosition(cellIndex);

                    bool hasBomb = bombGenerator.NextBool(newGridData.BombPercent);
                    if (hasBomb)
                    {
                        cell.HasBomb = true;
                        BombCount++;
                    }

                    CellIndexDict.Add(cell, cellIndex);
                    Cells.Add(cell);
                }
            }

            // centering the grid
            transform.localPosition = new Vector3(-gridSize.columnCount / 2f, -gridSize.rowCount / 2f, 0);
        }

        private void InitCellsContent()
        {
            foreach (ICell cell in Cells)
            {
                cell.SetContent(cell.HasBomb ? BombSprite : NoBombSprites[_gridBrain.GetNeighborBombCount(cell)]);
            }
        }
        
        #region Cell Highlight

        private void UpdateGridHighlight(ICell cellHovering)
        {
            RemoveGridHighlight();
            if (cellHovering == null)
            {
                return;
            }

            List<ICell> affectedCells = _gridBrain.GetAffectedCellsWithinScope(cellHovering, playerState.SweepScope);
            foreach (ICell cell in affectedCells)
            {
                cell.SetHighlight(true);
            }
        }

        private void RemoveGridHighlight()
        {
            foreach (ICell cell in Cells)
            {
                cell.SetHighlight(false);
            }
        }

        #endregion
    }
}