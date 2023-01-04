using System;
using SnekTech.Player;
using SnekTech.UI;
using UnityEngine;

namespace SnekTech.Core.GameEvent
{
    public abstract class CellEventData : ScriptableObject, IHoverableIconHolder
    {
        [SerializeField]
        private string label;

        [SerializeField]
        private Sprite icon;

        [SerializeField]
        private string description;

        public string Label => label;
        public Sprite Icon => icon;
        public string Description => description;

        public void Trigger(PlayerState playerState)
        {
            OnTrigger(playerState);
        }

        protected abstract void OnTrigger(PlayerState playerState);
    }
}
