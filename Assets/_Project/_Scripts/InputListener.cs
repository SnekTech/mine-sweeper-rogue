using System.Collections.Generic;
using System.Threading.Tasks;
using SnekTech.Grid;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SnekTech
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputListener : MonoBehaviour
    {
        public InputEventManager inputEventManager;

        private PlayerInput _playerInput;
        private InputAction _leftClickAction;
        private InputAction _rightClickAction;
        private InputAction _moveAction;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _leftClickAction = _playerInput.actions["LeftClick"];
            _rightClickAction = _playerInput.actions["RightClick"];
            _moveAction = _playerInput.actions["Move"];
        }

        private void OnEnable()
        {
            _leftClickAction.performed += OnLeftClickPerformed;
            _rightClickAction.performed += OnRightClickPerformed;
        }

        private void OnDisable()
        {
            _leftClickAction.performed -= OnLeftClickPerformed;
            _rightClickAction.performed -= OnRightClickPerformed;
        }

        private void OnLeftClickPerformed(InputAction.CallbackContext obj)
        {
            var mousePosition = _moveAction.ReadValue<Vector2>();
            inputEventManager.OnLeftClickPerformed(mousePosition);
        }

        private void OnRightClickPerformed(InputAction.CallbackContext obj)
        {
            var mousePosition = _moveAction.ReadValue<Vector2>();
            inputEventManager.OnRightClickPerformed(mousePosition);
        }
    }
}