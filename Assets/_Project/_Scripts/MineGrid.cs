using System.Collections.Generic;
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

        private readonly List<ICell> _cells = new List<ICell>();
        
        private PlayerInput _playerInput;
        private InputAction _leftClickAction;
        private InputAction _rightClickAction;
        private InputAction _moveAction;
        private Camera _mainCamera;

        private int _cellLayer;

        private const int MineGeneratorSeed = 0;
        private ISequence<bool> _mineGenerator;
        
        
        private void Awake()
        {
            _mineGenerator = new RandomBoolSequence(MineGeneratorSeed);
            
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
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, ~_cellLayer);
            
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
            return GetComponentInChildren<ICell>() != null;
        }

        private void InstantiateCells()
        {
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    CellBehaviour cell = Instantiate(cellBehaviour, transform);
                    cell.transform.localPosition = new Vector3(x, y, 0);
                    _cells.Add(cell);
                }
            }
        }

        private void ResetCells()
        {
            foreach (ICell cell in _cells)
            {
                cell.Reset();
            }
        }
    
    }
}
