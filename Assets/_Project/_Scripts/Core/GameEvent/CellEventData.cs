using SnekTech.Player;
using UnityEngine;

namespace SnekTech.Core.GameEvent
{
    public class CellEventData : ScriptableObject
    {
        [SerializeField]
        private string label;

        [SerializeField]
        private Sprite icon;

        [SerializeField]
        private string description;

        protected bool IsActive = true;

        public void Emit(PlayerState playerState)
        {
            OnEmit(playerState);
        }

        protected virtual void OnEmit(PlayerState playerState)
        {
            
        }
    }
}
