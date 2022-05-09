using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SnekTech.GridCell;
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
        private GridEventManager gridEventManager;

        private Camera _mainCamera;
        private int _cellLayer;

        private ISequence<bool> _bombGenerator;
        private IGridBrain _gridBrain;


        private List<Sprite> NoBombSprites => cellSprites.noBombSprites;
        private Sprite BombSprite => cellSprites.bombSprite;

        public Dictionary<ICell, GridIndex> CellIndexDict { get; } = new Dictionary<ICell, GridIndex>();
        public List<ICell> Cells { get; } = new List<ICell>();

        public GridData GridData { get; private set; } = GridData.Default;

        private GridSize GridSize => GridData.GridSize;
        public int CellCount => GridSize.rowCount * GridSize.columnCount;
        public int BombCount { get; private set; }

        public int RevealedCellCount => Cells.Count(cell => cell.IsRevealed);
        public int FlaggedCellCount => Cells.Count(cell => cell.IsFlagged);

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
            inputEventManager.RightClickPerformed += OnRightClickAsync;
        }

        private void DisableEventListeners()
        {
            inputEventManager.LeftClickPerformed -= OnLeftClickAsync;
            inputEventManager.RightClickPerformed -= OnRightClickAsync;
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

            bool isLeftClickSucceeded = await cell.OnLeftClick();
            if (!isLeftClickSucceeded)
            {
                return;
            }
            gridEventManager.InvokeEmptyCellRevealed(this);

            if (cell.HasBomb)
            {
                gridEventManager.InvokeBombRevealed(this);
                return;
            }

            if (_gridBrain.GetNeighborBombCount(cell) > 0)
            {
                return;
            }

            var leftClickNeighborTasks = new List<Task>();
            _gridBrain.ForEachNeighbor(cell, neighborCell =>
            {
                leftClickNeighborTasks.Add(RevealCellAsync(CellIndexDict[neighborCell]));
            });

            await Task.WhenAll(leftClickNeighborTasks);
        }

        public async void OnLeftClickAsync(Vector2 mousePosition)
        {
            ICell cell = GetClickedCell(mousePosition);
            if (cell == null)
            {
                return;
            }
            await RevealCellAsync(CellIndexDict[cell]);
            
            if (RevealedCellCount == CellCount - BombCount)
            {
                gridEventManager.InvokeGridCleared(this);
            }
        }

        public async void OnRightClickAsync(Vector2 mousePosition)
        {
            ICell cell = GetClickedCell(mousePosition);
            if (cell == null)
            {
                return;
            }

            await cell.OnRightClick();
            gridEventManager.InvokeCellFlagOperated(this);
        }

        private ICell GetClickedCell(Vector2 mousePosition)
        {
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

        public void InitCells(GridData gridData)
        {
            GridData = gridData;
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

        private void InstantiateCells(GridData gridData)
        {
            DisposeCells();

            _bombGenerator = new RandomBombGenerator(gridData.BombGeneratorSeed, gridData.BombPercent);
            BombCount = 0;
            
            GridSize gridSize = gridData.GridSize;
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

    }
}