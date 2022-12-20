using System;
using SnekTech.Player;
using UnityEngine;

namespace SnekTech.Core.GameEvent
{
    public class CellEventData : ScriptableObject
    {
        public event Action Completed;
        
        [SerializeField]
        private string label;

        [SerializeField]
        private Sprite icon;

        [SerializeField]
        private string description;

        public void Trigger(PlayerState playerState)
        {
            OnTrigger(playerState);
        }

        protected virtual void OnTrigger(PlayerState playerState)
        {
            
        }

        protected void InvokeCompleted()
        {
            Completed?.Invoke();
        }
    }
}
