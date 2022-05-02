using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SnekTech.GridCell;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SnekTech
{
    public class MineGrid : MonoBehaviour
    {
        [SerializeField]
        private CellBehaviour cellBehaviour;

        [SerializeField]
        private Vector2Int size = new Vector2Int(10, 10);

        [SerializeField]
        private CellSprites cellSprites;

        private readonly List<ICell> _cells = new List<ICell>();
        
        private PlayerInput _playerInput;
        private InputAction _leftClickAction;
        private InputAction _rightClickAction;
        private InputAction _moveAction;
        private Camera _mainCamera;

        private int _cellLayer;

        private const int BombGeneratorSeed = 0;
        private ISequence<bool> _bombGenerator;

        private static readonly Index2D[] NeighborOffsets = {
            new Index2D(-1, -1),
            new Index2D(0, -1),
            new Index2D(1, -1),
            new Index2D(-1, 0),
            new Index2D(1, 0),
            new Index2D(-1, 1),
            new Index2D(0, 1),
            new Index2D(1, 1),
        };

        private readonly Dictionary<ICell, Index2D> _cellIndexDict = new Dictionary<ICell, Index2D>();

        private int Width => size.x;
        private int Height => size.y;

        private List<Sprite> NoBombSprites => cellSprites.noBombSprites;
        private Sprite BombSprite => cellSprites.bombSprite;

        private Sprite GetSpriteSurroundedBy(int neighborBombCount)
        {
            if (neighborBombCount < 0 || neighborBombCount >= NoBombSprites.Count)
            {
                throw new RuntimeWrappedException($"Cannot get a cell sprite surrounded by {neighborBombCount} bombs.");
            }

            return NoBombSprites[neighborBombCount];
        }

        private void Awake()
        {
            _bombGenerator = new RandomBoolSequence(BombGeneratorSeed);
            
            _mainCamera = Camera.main;
            _cellLayer = 1 << LayerMask.NameToLayer("Cell");
            
            CachePlayerInputRelatedFields();
        }
        
        // Start is called before the first frame update
        private void Start()
        {
            
            InstantiateCells();
            SetCellsContent();
        }

        private void OnEnable()
        {
            EnablePlayerInput();
        }

        private void OnDisable()
        {
            DisablePlayerInput();
        }
        
        private void OnGridLeftClick(InputAction.CallbackContext obj)
        {
            ICell cell = GetClickedCell();
            cell?.OnLeftClick();
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

        [ContextMenu(nameof(InitCells))]
        private void InitCells()
        {
            if (!HasCells)
            {
                InstantiateCells();
            }
            else
            {
                ResetCells();
            }
        }

        private bool HasCells => _cells.Count > 0;

        [ContextMenu(nameof(ClearCells))]
        private void ClearCells()
        {
            foreach (ICell cell in _cells)
            {
                cell.Dispose();
            }
            _cells.Clear();
            _cellIndexDict.Clear();
        }

        private void InstantiateCells()
        {
            InstantiateCells(size);
        }

        private void InstantiateCells(Vector2Int gridSize)
        {
            InstantiateCells(gridSize.x, gridSize.y);
        }
        
        private void InstantiateCells(int rowCount, int columnCount)
        {
            ClearCells();
            
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    CellBehaviour cellMono = Instantiate(cellBehaviour, transform);
                    ICell cell = cellMono;
                    var cellIndex = new Index2D(i, j);
                    cell.SetPosition(cellIndex);
                    
                    bool hasBomb = _bombGenerator.Next();
                    if (hasBomb)
                    {
                        cell.HasBomb = true;
                    }
                    
                    _cellIndexDict.Add(cell, cellIndex);
                    _cells.Add(cell);
                }
            }
        }

        private void SetCellsContent()
        {
            foreach (ICell cell in _cells)
            {
                if (cell.HasBomb)
                {
                    cell.SetContent(BombSprite);
                }
                else
                {
                    cell.SetContent(GetSpriteSurroundedBy(GetNeighborBombCount(cell)));
                }
            }
        }

        private int GetNeighborBombCount(ICell cell)
        {
            Index2D cellIndex = _cellIndexDict[cell];
            TryCheckIndex(cellIndex);
            
            int neighborBombCount = 0;
            foreach (Index2D offset in NeighborOffsets)
            {
                Index2D index = cellIndex + offset;
                if (IsIndexWithinGrid(index) && GetCellAt(index).HasBomb)
                {
                    neighborBombCount++;
                }
            }

            return neighborBombCount;
        }

        private void ResetCells()
        {
            foreach (ICell cell in _cells)
            {
                cell.Reset();
            }
        }

        private ICell GetCellAt(Index2D index2D)
        {
            return GetCellAt(index2D.RowIndex, index2D.ColumnIndex);
        }
        
        private ICell GetCellAt(int rowIndex, int columnIndex)
        {
            TryCheckIndex(rowIndex, columnIndex);

            return _cells[rowIndex * Width + columnIndex];
        }

        private bool IsIndexWithinGrid(Index2D index2D)
        {
            return IsIndexWithinGrid(index2D.RowIndex, index2D.ColumnIndex);
        }

        private bool IsIndexWithinGrid(int rowIndex, int columnIndex)
        {
            return rowIndex >= 0 && rowIndex < size.y && columnIndex >= 0 && columnIndex < size.x;
        }

        private void TryCheckIndex(Index2D index)
        {
            TryCheckIndex(index.RowIndex, index.ColumnIndex);
        }

        private void TryCheckIndex(int rowIndex, int columnIndex)
        {
            if (!IsIndexWithinGrid(rowIndex, columnIndex))
            {
                throw new RuntimeWrappedException($"invalid grid index: ({rowIndex}, {columnIndex})");
            }
        }
    }
}
