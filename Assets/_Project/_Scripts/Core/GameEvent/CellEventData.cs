using System;
using SnekTech.GamePlay;
using SnekTech.GamePlay.EffectSystem;
using SnekTech.GamePlay.PlayerSystem;
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

        public void Trigger(Player player)
        {
            OnTrigger(player);
        }

        protected abstract void OnTrigger(Player player);
    }
}
