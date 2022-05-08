using System.Collections.Generic;
using System.Threading.Tasks;
using SnekTech.Grid;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SnekTech
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoBehaviour
    {
        public List<GridBehaviour> grids = new List<GridBehaviour>();

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

        private async void OnLeftClickPerformed(InputAction.CallbackContext obj)
        {
            var mousePosition = _moveAction.ReadValue<Vector2>();
            var clickTasks = new List<Task>();
            foreach (ICanClickAsync clickable in grids)
            {
                clickTasks.Add(clickable.OnLeftClickAsync(mousePosition));
            }

            await Task.WhenAll(clickTasks);
        }

        private async void OnRightClickPerformed(InputAction.CallbackContext obj)
        {
            var mousePosition = _moveAction.ReadValue<Vector2>();
            var clickTasks = new List<Task>();
            foreach (ICanClickAsync clickable in grids)
            {
                clickTasks.Add(clickable.OnRightClickAsync(mousePosition));
            }

            await Task.WhenAll(clickTasks);
        }
    }
}