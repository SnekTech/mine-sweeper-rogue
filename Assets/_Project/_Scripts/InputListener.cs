using SnekTech.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SnekTech
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputListener : MonoBehaviour
    {
        [SerializeField]
        private InputEventManager inputEventManager;

        private PlayerInput _playerInput;
        private InputAction _leftClickAction;
        private InputAction _leftDoubleClickAction;
        private InputAction _rightClickAction;
        private InputAction _moveAction;
        private InputAction _pauseAction;

        private Vector2 MousePosition => _moveAction.ReadValue<Vector2>();
        public Vector2 MouseCanvasPosition => MousePosition / CanvasInfo.Instance.ScaleFactor;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _leftClickAction = _playerInput.actions["LeftClick"];
            _leftDoubleClickAction = _playerInput.actions["LeftDoubleClick"];
            _rightClickAction = _playerInput.actions["RightClick"];
            _moveAction = _playerInput.actions["Move"];
            _pauseAction = _playerInput.actions["Pause"];
        }

        private void OnEnable()
        {
            _leftClickAction.performed += OnLeftClickPerformed;
            _leftDoubleClickAction.performed += OnLeftDoubleClickPerformed;
            _rightClickAction.performed += OnRightClickPerformed;
            _moveAction.performed += OnMovePerformed;
            _pauseAction.performed += OnPausePerformed;
        }

        private void OnDisable()
        {
            _leftClickAction.performed -= OnLeftClickPerformed;
            _leftDoubleClickAction.performed -= OnLeftDoubleClickPerformed;
            _rightClickAction.performed -= OnRightClickPerformed;
            _moveAction.performed -= OnMovePerformed;
            _pauseAction.performed -= OnPausePerformed;
        }

        private void OnLeftClickPerformed(InputAction.CallbackContext obj)
        {
            inputEventManager.InvokeLeftClickPerformed(MousePosition);
        }

        private void OnLeftDoubleClickPerformed(InputAction.CallbackContext obj)
        {
            inputEventManager.InvokeLeftDoubleClickPerformed(MousePosition);
        }

        private void OnRightClickPerformed(InputAction.CallbackContext obj)
        {
            inputEventManager.InvokeRightClickPerformed(MousePosition);
        }

        private void OnMovePerformed(InputAction.CallbackContext obj)
        {
            inputEventManager.InvokeMovePerformed(MousePosition);
        }

        private void OnPausePerformed(InputAction.CallbackContext obj)
        {
            inputEventManager.InvokePausePerformed();
        }
    }
}