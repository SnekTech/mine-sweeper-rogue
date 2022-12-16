using System;
using SnekTech.Constants;
using SnekTech.SceneManagement;
using UnityEngine;

namespace SnekTech
{
    [CreateAssetMenu(fileName = nameof(InputEventManager), menuName = MenuName.EventManager + MenuName.Slash + nameof(InputEventManager))]
    public class InputEventManager : ScriptableObject
    {
        public event Action<Vector2> LeftClickPerformed;
        public event Action<Vector2> LeftDoubleClickPerformed;
        public event Action<Vector2> RightClickPerformed;
        public event Action<Vector2> MovePerformed; 
        public event Action PausePerformed;

        [SerializeField]
        private MySceneManager mySceneManager;

        public void InvokeLeftClickPerformed(Vector2 mousePosition)
        {
            LeftClickPerformed?.Invoke(mousePosition);
        }

        public void InvokeLeftDoubleClickPerformed(Vector2 mousePosition)
        {
            LeftDoubleClickPerformed?.Invoke(mousePosition);
        }

        public void InvokeRightClickPerformed(Vector2 mousePosition)
        {
            RightClickPerformed?.Invoke(mousePosition);
        }

        public void InvokeMovePerformed(Vector2 mousePosition)
        {
            MovePerformed?.Invoke(mousePosition);
        }

        public void InvokePausePerformed()
        {
            if (mySceneManager.CurrentScene != SceneIndex.Game)
            {
                return;
            }
            PausePerformed?.Invoke();
        }
    }
}
