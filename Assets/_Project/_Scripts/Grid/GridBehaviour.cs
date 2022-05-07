using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SnekTech.GridCell;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SnekTech.Grid
{
    public class GridBehaviour : MonoBehaviour, IGrid
    {
        [SerializeField]
        private CellBehaviour cellBehaviour;
        [SerializeField]
        private CellSprites cellSprites;

        private PlayerInput _playerInput;
        private InputAction _leftClickAction;
        private InputAction _rightClickAction;
        private InputAction _moveAction;
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

            CachePlayerInputRelatedFields();
        }

        private void Start()
        {
            InitCells();
        }

        private void OnEnable()
        {
            EnablePlayerInput();
        }

        private void OnDisable()
        {
            DisablePlayerInput();
        }

        private async void OnGridLeftClick(InputAction.CallbackContext obj)
        {
            ICell cell = GetClickedCell();
            if (cell == null)
            {
                return;
            }

            await RevealCellAsync(CellIndexDict[cell]);
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

        private void OnGridRightClick(InputAction.CallbackContext context)
        {
            ICell cell = GetClickedCell();
            cell?.OnRightClick();
        }

        private ICell GetClickedCell()
        {
            var mousePosition = _moveAction.ReadValue<Vector2>();
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, _cellLayer);

            return hit.collider != null ? hit.collider.GetComponent<ICell>() : null;
        }

        private void CachePlayerInputRelatedFields()
        {
            _playerInput = GetComponent<PlayerInput>();
            _leftClickAction = _playerInput.actions["LeftClick"];
            _rightClickAction = _playerInput.actions["RightClick"];
            _moveAction = _playerInput.actions["Move"];
        }

        private void EnablePlayerInput()
        {
            _leftClickAction.performed += OnGridLeftClick;
            _rightClickAction.performed += OnGridRightClick;
        }

        private void DisablePlayerInput()
        {
            _leftClickAction.performed -= OnGridLeftClick;
            _rightClickAction.performed -= OnGridRightClick;
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