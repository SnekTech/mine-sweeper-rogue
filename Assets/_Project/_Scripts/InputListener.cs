using SnekTech.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SnekTech
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputListener : MonoBehaviour
    {
        private static class ActionNames
        {
            public const string Primary = "Primary";
            public const string Secondary = "Secondary";
            public const string DoublePrimary = "DoublePrimary";
            public const string Move = "Move";
            public const string Pause = "Pause";
        }
        
        [SerializeField]
        private InputEventChannel inputEventChannel;

        private PlayerInput _playerInput;
        private InputAction _primaryAction;
        private InputAction _doublePrimaryAction;
        private InputAction _secondaryAction;
        private InputAction _moveAction;
        private InputAction _pauseAction;

        private Vector2 MousePosition => _moveAction.ReadValue<Vector2>();
        public Vector2 MouseCanvasPosition => MousePosition / CanvasInfo.Instance.ScaleFactor;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _primaryAction = _playerInput.actions[ActionNames.Primary];
            _doublePrimaryAction = _playerInput.actions[ActionNames.DoublePrimary];
            _secondaryAction = _playerInput.actions[ActionNames.Secondary];
            _moveAction = _playerInput.actions[ActionNames.Move];
            _pauseAction = _playerInput.actions[ActionNames.Pause];
        }

        private void OnEnable()
        {
            _primaryAction.performed += OnPrimaryPerformed;
            _doublePrimaryAction.performed += OnDoublePrimaryPerformed;
            _secondaryAction.performed += OnSecondaryPerformed;
            _moveAction.performed += OnMovePerformed;
            _pauseAction.performed += OnPausePerformed;
        }

        private void OnDisable()
        {
            _primaryAction.performed -= OnPrimaryPerformed;
            _doublePrimaryAction.performed -= OnDoublePrimaryPerformed;
            _secondaryAction.performed -= OnSecondaryPerformed;
            _moveAction.performed -= OnMovePerformed;
            _pauseAction.performed -= OnPausePerformed;
        }

        private void OnPrimaryPerformed(InputAction.CallbackContext obj)
        {
            inputEventChannel.InvokePrimaryPerformed(MousePosition);
        }

        private void OnDoublePrimaryPerformed(InputAction.CallbackContext obj)
        {
            inputEventChannel.InvokeDoublePrimaryPerformed(MousePosition);
        }

        private void OnSecondaryPerformed(InputAction.CallbackContext obj)
        {
            inputEventChannel.InvokeSecondaryPerformed(MousePosition);
        }

        private void OnMovePerformed(InputAction.CallbackContext obj)
        {
            inputEventChannel.InvokeMovePerformed(MousePosition);
        }

        private void OnPausePerformed(InputAction.CallbackContext obj)
        {
            inputEventChannel.InvokePausePerformed();
        }
    }
}