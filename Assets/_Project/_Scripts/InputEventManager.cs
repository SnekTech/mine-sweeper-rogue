using System;
using UnityEngine;

namespace SnekTech
{
    [CreateAssetMenu(fileName = nameof(InputEventManager), menuName = "MyEventManagers/" + nameof(InputEventManager))]
    public class InputEventManager : ScriptableObject
    {
        public event Action<Vector2> LeftClickPerformed;
        public event Action<Vector2> RightClickPerformed;

        public void OnLeftClickPerformed(Vector2 mousePosition)
        {
            LeftClickPerformed?.Invoke(mousePosition);
        }

        public void OnRightClickPerformed(Vector2 mousePosition)
        {
            RightClickPerformed?.Invoke(mousePosition);
        }
    }
}
