using System;
using System.Collections.Generic;
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

        private List<Cell> _cells = new List<Cell>();
        
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
            
            InitPlayerInput();
        }
        
        // Start is called before the first frame update
        private void Start()
        {
            
            InitCells();
        }
        
        private void OnDisable()
        {
            DisablePlayerInput();
        }
        
        private void OnCellLeftClick(InputAction.CallbackContext obj)
        {
            Cell cell = GetClickedCell();
            
        }

        private void OnCellRightClick(InputAction.CallbackContext context)
        {
            Cell cell = GetClickedCell();
        }

        private Cell GetClickedCell()
        {
            var mousePosition = _moveAction.ReadValue<Vector2>();
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, float.MaxValue, _cellLayer);
            
            return hit.collider != null ? hit.collider.GetComponent<Cell>() : null;
        }

        private void InitPlayerInput()
        {
            _playerInput = GetComponent<PlayerInput>();
            _leftClickAction = _playerInput.actions["LeftClick"];
            _rightClickAction = _playerInput.actions["RightClick"];
            _moveAction = _playerInput.actions["Move"];
            _leftClickAction.performed += OnCellLeftClick;
            _rightClickAction.performed += OnCellRightClick;
        }
        
        private void DisablePlayerInput()
        {
            _leftClickAction.performed -= OnCellLeftClick;
            _rightClickAction.performed -= OnCellRightClick;
        }

        [ContextMenu(nameof(InitCells))]
        private void InitCells()
        {
            _cells.Clear();
        
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
    
    }
}
