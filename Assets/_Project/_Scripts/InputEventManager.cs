using System;
using SnekTech.C;
using SnekTech.SceneManagement;
using UnityEngine;

namespace SnekTech
{
    [CreateAssetMenu(fileName = nameof(InputEventManager), menuName = MenuName.EventManagers + "/" + nameof(InputEventManager))]
    public class InputEventManager : ScriptableObject
    {
        public event Action<Vector2> PrimaryPerformed;
        public event Action<Vector2> DoublePrimaryPerformed;
        public event Action<Vector2> SecondaryPerformed;
        public event Action<Vector2> MovePerformed; 
        public event Action PausePerformed;

        [SerializeField]
        private MySceneManager mySceneManager;

        public void InvokePrimaryPerformed(Vector2 mousePosition)
        {
            PrimaryPerformed?.Invoke(mousePosition);
        }

        public void InvokeDoublePrimaryPerformed(Vector2 mousePosition)
        {
            DoublePrimaryPerformed?.Invoke(mousePosition);
        }

        public void InvokeSecondaryPerformed(Vector2 mousePosition)
        {
            SecondaryPerformed?.Invoke(mousePosition);
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
