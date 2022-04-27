using System.Collections.Generic;
using SnekTech.GridCell;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SnekTech
{
    public class MineGrid : MonoBehaviour
    {
        [SerializeField]
        private Cell cellPrefab;

        [SerializeField]
        private Vector2Int size = new Vector2Int(10, 10);

        private readonly List<Cell> _cells = new List<Cell>();
        
        private PlayerInput _playerInput;
        private InputAction _leftClickAction;
        private InputAction _rightClickAction;
        private InputAction _moveAction;
        private Camera _mainCamera;

        private int _cellLayer;
        
        
        private void Awake()
        {
            _mainCamera = Camera.main;
            _cellLayer = LayerMask.NameToLayer("Cell");
            
            CachePlayerInputRelatedFields();
        }
        
        // Start is called before the first frame update
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
        
        private void OnGridLeftClick(InputAction.CallbackContext obj)
        {
            Cell cell = GetClickedCell();
            if (cell != null)
            {
                cell.OnLeftClick();
            }
        }

        private void OnGridRightClick(InputAction.CallbackContext context)
        {
            Cell cell = GetClickedCell();
            if (cell != null)
            {
                cell.OnRightClick();
            }
        }

        private Cell GetClickedCell()
        {
            var mousePosition = _moveAction.ReadValue<Vector2>();
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, ~_cellLayer);
            
            return hit.collider != null ? hit.collider.GetComponent<Cell>() : null;
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
            if (!HasCells())
            {
                InstantiateCells();
            }
            else
            {
                ResetCells();
            }
        }

        private bool HasCells()
        {
            return GetComponentInChildren<Cell>() != null;
        }

        private void InstantiateCells()
        {
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    Cell cell = Instantiate(cellPrefab, transform);
                    cell.transform.localPosition = new Vector3(x, y, 0);
                    _cells.Add(cell);
                }
            }
        }

        private void ResetCells()
        {
            foreach (Cell cell in _cells)
            {
                cell.Reset();
            }
        }
    
    }
}
