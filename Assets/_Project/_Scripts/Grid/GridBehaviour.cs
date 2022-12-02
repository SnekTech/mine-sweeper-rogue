using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SnekTech.GridCell;
using SnekTech.Player;
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
        private GridData gridData;

        [SerializeField]
        private PlayerState playerState;

        [SerializeField]
        private UIState uiState;

        private Camera _mainCamera;
        private int _cellLayer;

        private ISequence<bool> _bombGenerator;
        private IGridBrain _gridBrain;


        private List<Sprite> NoBombSprites => cellSprites.noBombSprites;
        private Sprite BombSprite => cellSprites.bombSprite;

        public Dictionary<ICell, GridIndex> CellIndexDict { get; } = new Dictionary<ICell, GridIndex>();
        public List<ICell> Cells { get; } = new List<ICell>();

        public GridData GridData
        {
            get => gridData;
            set => gridData = value;
        }

        private GridSize GridSize => GridData.GridSize;
        public int CellCount => GridSize.rowCount * GridSize.columnCount;
        public int BombCount { get; private set; }

        public int RevealedCellCount => Cells.Count(cell => cell.IsRevealed);
        public int FlaggedCellCount => Cells.Count(cell => cell.IsFlagged);

        private bool IsAllCleared => RevealedCellCount == CellCount - BombCount;

        private void Awake()
        {
            _gridBrain = new BasicGridBrain(this);

            _mainCamera = Camera.main;
            _cellLayer = 1 << LayerMask.NameToLayer("Cell");
        }

        private void Start()
        {
            InitCells();
        }

        private void OnEnable()
        {
            EnableEventListeners();
        }

        private void OnDisable()
        {
            DisableEventListeners();
        }

        private void EnableEventListeners()
        {
            inputEventManager.LeftClickPerformed += OnLeftClickAsync;
            inputEventManager.LeftDoubleClickPerformed += OnLeftDoubleClickAsync;
            inputEventManager.RightClickPerformed += OnRightClickAsync;
            inputEventManager.MovePerformed += OnMove;

            uiEventManager.ResetButtonClicked += OnResetButtonClicked;
        }

        private void DisableEventListeners()
        {
            inputEventManager.LeftClickPerformed -= OnLeftClickAsync;
            inputEventManager.LeftDoubleClickPerformed -= OnLeftDoubleClickAsync;
            inputEventManager.RightClickPerformed -= OnRightClickAsync;
            inputEventManager.MovePerformed -= OnMove;

            uiEventManager.ResetButtonClicked -= OnResetButtonClicked;
        }

        private void OnResetButtonClicked(GridData gridDataIn)
        {
            InitCells(gridDataIn);
        }

        public async void OnLeftClickAsync(Vector2 mousePosition)
        {
            ICell cell = GetMouseHoveringCell(mousePosition);
            if (cell == null)
            {
                return;
            }

            List<ICell> affectedCells = _gridBrain.GetAffectedCellsWithinScope(cell, playerState.SweepScope);
            List<Task> revealCellTasks = new List<Task>();
            foreach (ICell affectedCell in affectedCells)
            {
                revealCellTasks.Add(RevealCellAsync(CellIndexDict[affectedCell]));
            }

            await Task.WhenAll(revealCellTasks);

            if (IsAllCleared)
            {
                gridEventManager.InvokeGridCleared(this);
            }
        }

        public async void OnLeftDoubleClickAsync(Vector2 mousePosition)
        {
            ICell cell = GetMouseHoveringCell(mousePosition);
            if (cell == null || !cell.IsRevealed || cell.HasBomb)
            {
                return;
            }

            int userConfirmedBombCount = 0;
            _gridBrain.ForEachNeighbor(cell, neighborCell =>
            {
                if (neighborCell.IsFlagged || neighborCell.HasBomb && neighborCell.IsRevealed)
                {
                    userConfirmedBombCount++;
                }
            });

            int neighborBombCount = _gridBrain.GetNeighborBombCount(cell);
            if (userConfirmedBombCount != neighborBombCount)
            {
                return;
            }

            await RevealNeighbors(cell);
        }

        public async void OnRightClickAsync(Vector2 mousePosition)
        {
            ICell cell = GetMouseHoveringCell(mousePosition);
            if (cell == null)
            {
                return;
            }

            await cell.OnRightClick();
            gridEventManager.InvokeCellFlagOperated(this);
        }

        private void OnMove(Vector2 mousePosition)
        {
            ICell cellHovering = GetMouseHoveringCell(mousePosition);
            UpdateGridHighlight(cellHovering);
        }
        
        private async Task RevealCellAsync(GridIndex cellGridIndex)
        {
            if (!_gridBrain.IsIndexWithinGrid(cellGridIndex))
            {
                return;
            }

            ICell cell = _gridBrain.GetCellAt(cellGridIndex);
            if (!cell.IsCovered)
            {
                return;
            }

            // if last click action has not finished,
            // the task will return false,
            // to throttle the player input frequency
            bool isLeftClickSucceeded = await cell.OnLeftClick();
            if (!isLeftClickSucceeded)
            {
                return;
            }

            gridEventManager.InvokeEmptyCellRevealed(this);

            if (cell.HasBomb)
            {
                gridEventManager.InvokeBombRevealed(this, cell);
                return;
            }

            if (_gridBrain.GetNeighborBombCount(cell) > 0)
            {
                return;
            }

            await RevealNeighbors(cell);
        }

        private Task RevealNeighbors(ICell cell)
        {
            var revealNeighborTasks = new List<Task>();
            _gridBrain.ForEachNeighbor(cell,
                neighborCell =>
                {
                    revealNeighborTasks.Add(RevealCellAsync(CellIndexDict[neighborCell]));
                });

            return Task.WhenAll(revealNeighborTasks);
        }

        private ICell GetMouseHoveringCell(Vector2 mousePosition)
        {
            if (uiState.isBlockingRaycast)
            {
                return null;
            }
            
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, _cellLayer);

            return hit.collider != null ? hit.collider.GetComponent<ICell>() : null;
        }

        public void InitCells()
        {
            InstantiateCells(GridData);
            InitCellsContent();
            gridEventManager.InvokeGridInitCompleted(this);
        }

        public void InitCells(GridData newGridData)
        {
            GridData = newGridData;
            InitCells();
        }

        [ContextMenu(nameof(DisposeCells))]
        public void DisposeCells()
        {
            foreach (ICell cell in Cells)
            {
                cell.Dispose();
            }

            Cells.Clear();
            CellIndexDict.Clear();
        }

        private void InstantiateCells(GridData newGridData)
        {
            DisposeCells();

            _bombGenerator = new RandomBombGenerator(newGridData.BombGeneratorSeed, newGridData.BombPercent);
            BombCount = 0;

            GridSize gridSize = newGridData.GridSize;
            for (int i = 0; i < gridSize.rowCount; i++)
            {
                for (int j = 0; j < gridSize.columnCount; j++)
                {
                    CellBehaviour cellMono = Instantiate(cellBehaviour, transform);
                    ICell cell = cellMono;
                    var cellIndex = new GridIndex(i, j);
                    cell.SetPosition(cellIndex);

                    bool hasBomb = _bombGenerator.Next();
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
                if (cell.HasBomb)
                {
                    cell.SetContent(BombSprite);
                }
                else
                {
                    cell.SetContent(NoBombSprites[_gridBrain.GetNeighborBombCount(cell)]);
                }
            }
        }

        public void ResetCells()
        {
            foreach (ICell cell in Cells)
            {
                cell.Reset();
            }
        }

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
                cell.SetCoverHighlight(true);
            }
        }

        private void RemoveGridHighlight()
        {
            foreach (ICell cell in Cells)
            {
                cell.SetCoverHighlight(false);
            }
        }
    }
}