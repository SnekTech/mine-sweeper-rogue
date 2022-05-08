using System;
using System.Collections.Generic;
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

        private Camera _mainCamera;

        private int _cellLayer;

        private const int BombGeneratorSeed = 0;
        private ISequence<bool> _bombGenerator;

        private IGridBrain _gridBrain;


        private List<Sprite> NoBombSprites => cellSprites.noBombSprites;
        private Sprite BombSprite => cellSprites.bombSprite;

        public Dictionary<ICell, GridIndex> CellIndexDict { get; } = new Dictionary<ICell, GridIndex>();
        public List<ICell> Cells { get; } = new List<ICell>();
        public GridSize Size { get; private set; } = new GridSize(10, 10);

        private Sprite GetSpriteByNeighbourBombCount(int neighborBombCount)
        {
            if (neighborBombCount < 0 || neighborBombCount >= NoBombSprites.Count)
            {
                throw new Exception($"Cannot get a cell sprite surrounded by {neighborBombCount} bombs.");
            }

            return NoBombSprites[neighborBombCount];
        }

        private void Awake()
        {
            _gridBrain = new BasicGridBrain(this);
            
            _bombGenerator = new RandomBombGenerator(BombGeneratorSeed, 0.1f);

            _mainCamera = Camera.main;
            _cellLayer = 1 << LayerMask.NameToLayer("Cell");
        }

        private void Start()
        {
            InitCells();
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

            if (cell.HasBomb || _gridBrain.GetNeighborBombCount(cell) > 0)
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

        public Task OnLeftClickAsync(Vector2 mousePosition)
        {
            ICell cell = GetClickedCell(mousePosition);
            return cell == null ? Task.CompletedTask : RevealCellAsync(CellIndexDict[cell]);
        }

        public Task OnRightClickAsync(Vector2 mousePosition)
        {
            ICell cell = GetClickedCell(mousePosition);
            return cell?.OnRightClick();
        }

        private ICell GetClickedCell(Vector2 mousePosition)
        {
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, _cellLayer);

            return hit.collider != null ? hit.collider.GetComponent<ICell>() : null;
        }

        public void InitCells()
        {
            InstantiateCells(Size);
            InitCellsContent();
        }

        public void InitCells(GridSize gridSize)
        {
            Size = gridSize;
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

        private void InstantiateCells(GridSize gridSize)
        {
            DisposeCells();

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
                    cell.SetContent(GetSpriteByNeighbourBombCount(_gridBrain.GetNeighborBombCount(cell)));
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