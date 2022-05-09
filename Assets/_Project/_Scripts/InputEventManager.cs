using System;
using UnityEngine;

namespace SnekTech
{
    [CreateAssetMenu(fileName = nameof(InputEventManager), menuName = Utils.MyEventManagerMenuName + nameof(InputEventManager))]
    public class InputEventManager : ScriptableObject
    {
        public event Action<Vector2> LeftClickPerformed;
        public event Action<Vector2> RightClickPerformed;

        public void InvokeLeftClickPerformed(Vector2 mousePosition)
        {
            LeftClickPerformed?.Invoke(mousePosition);
        }

        public void InvokeRightClickPerformed(Vector2 mousePosition)
        {
            RightClickPerformed?.Invoke(mousePosition);
        }
    }
}
