using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using SnekTech.GamePlay.PlayerSystem;
using SnekTech.GridCell;
using SnekTech.Roguelike;
using SnekTech.UI;
using UnityEngine;

namespace SnekTech.Grid
{
    public class GridBehaviour : MonoBehaviour, IGrid
    {
        [SerializeField]
        private Cell cellPrefab;

        [SerializeField]
        private CellSprites cellSprites;

        [SerializeField]
        private InputEventChannel inputEventChannel;

        [SerializeField]
        private GridEventChannel gridEventChannel;

        [SerializeField]
        private Player player;

        [SerializeField]
        private UIStateManager uiStateManager;

        [SerializeField]
        private Camera mainCamera;
        
        private int _cellLayer;

        private IGridBrain _gridBrain;


        private List<Sprite> NoBombSprites => cellSprites.noBombSprites;
        private Sprite BombSprite => cellSprites.bombSprite;

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
            inputEventChannel.PrimaryPerformed += HandlePrimary;
            inputEventChannel.DoublePrimaryPerformed += HandleDoublePrimary;
            inputEventChannel.SecondaryPerformed += HandleSecondary;
            inputEventChannel.MovePerformed += OnMove;
        }

        private void OnDisable()
        {
            inputEventChannel.PrimaryPerformed -= HandlePrimary;
            inputEventChannel.DoublePrimaryPerformed -= HandleDoublePrimary;
            inputEventChannel.SecondaryPerformed -= HandleSecondary;
            inputEventChannel.MovePerformed -= OnMove;
        }
        
        #endregion

        #region Event Handlers
        
        private void HandlePrimary(Vector2 mousePosition) => ProcessPrimaryAsync(mousePosition).Forget();

        private void HandleDoublePrimary(Vector2 mousePosition) =>
            ProcessDoublePrimaryAsync(mousePosition).Forget();

        private void HandleSecondary(Vector2 mousePosition) => ProcessSecondaryAsync(mousePosition).Forget();

        private void OnMove(Vector2 mousePosition)
        {
            var cellHovering = GetMouseHoveringCell(mousePosition);
            UpdateGridHighlight(cellHovering);
        }
        
        #endregion

        public async UniTaskVoid ProcessPrimaryAsync(Vector2 mousePosition)
        {
            var cell = GetMouseHoveringCell(mousePosition);
            bool canClickCell = cell is {IsCovered: true};
            if (!canClickCell)
            {
                return;
            }
            
            player.UseClickAbilities();
            
            await player.Weapon.Primary(cell);

            HandleRecursiveRevealCellComplete(cell);
        }

        public async UniTaskVoid ProcessDoublePrimaryAsync(Vector2 mousePosition)
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

        public async UniTaskVoid ProcessSecondaryAsync(Vector2 mousePosition)
        {
            var cell = GetMouseHoveringCell(mousePosition);
            if (cell == null)
            {
                return;
            }

            bool isClickSuccessful = await cell.SwitchFlag();

            if (isClickSuccessful)
            {
                gridEventChannel.InvokeOnCellFlagOperated(this);
            }
        }

        public async UniTask RevealCellAsync(GridIndex cellGridIndex)
        {
            if (!_gridBrain.IsIndexWithinGrid(cellGridIndex))
            {
                return;
            }

            var cell = GetCellAt(cellGridIndex);
            if (!cell.IsCovered)
            {
                return;
            }

            // if last click action has not finished,
            // the task will return false,
            // to throttle the player input frequency
            bool isLeftClickSuccessful = await cell.Reveal();
            if (!isLeftClickSuccessful)
            {
                return;
            }

            gridEventChannel.InvokeOnCellReveal(this, cell);

            if (cell.HasBomb)
            {
                gridEventChannel.InvokeOnBombReveal(this, cell);
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
                    revealNeighborTasks.Add(RevealCellAsync(neighborCell.GridIndex));
                });

            return UniTask.WhenAll(revealNeighborTasks);
        }

        private ICell GetMouseHoveringCell(Vector2 mousePosition)
        {
            if (uiStateManager.isBlockingRaycast)
            {
                return null;
            }
            
            var ray = mainCamera.ScreenPointToRay(mousePosition);
            var hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, _cellLayer);

            return hit.collider != null ? hit.collider.GetComponent<ICell>() : null;
        }

        private void HandleRecursiveRevealCellComplete(ILogicCell originalCell)
        {
            gridEventChannel.InvokeOnRecursiveRevealComplete(originalCell);
            CheckIfAllCleared();
        }

        private void CheckIfAllCleared()
        {
            if (IsAllCleared)
            {
                gridEventChannel.InvokeOnGridCleared(this);
            }
        }
        
        #region cell initialization

        private void InitCells()
        {
            InstantiateCells(GridData);
            InitCellsContent();
            gridEventChannel.InvokeOnGridInitComplete(this);
        }

        public void InitCells(GridData newGridData)
        {
            GridData = newGridData;
            InitCells();
        }

        private void ClearCells()
        {
            transform.DestroyAllChildren();

            Cells.Clear();
        }

        private void InstantiateCells(GridData newGridData)
        {
            ClearCells();

            IRandomGenerator bombGenerator = RandomGenerator.Instance;
            BombCount = 0;

            var gridSize = newGridData.GridSize;
            for (int i = 0; i < gridSize.rowCount; i++)
            {
                for (int j = 0; j < gridSize.columnCount; j++)
                {
                    var cell = Instantiate(cellPrefab, transform);
                    var cellIndex = new GridIndex(i, j);
                    cell.GridIndex = cellIndex;
                    cell.SetPosition(cellIndex);
                    cell.Grid = this;

                    bool hasBomb = bombGenerator.NextBool(newGridData.BombPercent);
                    if (hasBomb)
                    {
                        cell.HasBomb = true;
                        BombCount++;
                    }

                    Cells.Add(cell);
                }
            }

            // centering the grid
            transform.localPosition = new Vector3(-gridSize.columnCount / 2f, -gridSize.rowCount / 2f, 0);
        }

        private void InitCellsContent()
        {
            foreach (var cell in Cells)
            {
                cell.SetContent(cell.HasBomb ? BombSprite : NoBombSprites[_gridBrain.GetNeighborBombCount(cell)]);
            }
        }
        
        #endregion

        public ICell GetCellAt(GridIndex gridIndex)
        {
            return Cells[gridIndex.RowIndex * GridSize.columnCount + gridIndex.ColumnIndex];
        }

        #region Cell Highlight

        private void UpdateGridHighlight(ICell cellHovering)
        {
            RemoveGridHighlight();
            if (cellHovering == null)
            {
                return;
            }

            var affectedCells = _gridBrain.GetAffectedCellsWithinScope(cellHovering, player.SweepScope);
            foreach (var cell in affectedCells)
            {
                cell.SetHighlight(true);
            }
        }

        private void RemoveGridHighlight()
        {
            foreach (var cell in Cells)
            {
                cell.SetHighlight(false);
            }
        }

        #endregion
    }
}