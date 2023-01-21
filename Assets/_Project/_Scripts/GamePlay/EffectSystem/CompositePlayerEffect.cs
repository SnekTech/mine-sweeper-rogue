using System;
using System.Collections.Generic;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.EffectSystem
{
    [Serializable]
    public class CompositePlayerEffect : IPlayerEffect
    {
        [SerializeReference]
        private List<IPlayerEffect> playerEffects = new List<IPlayerEffect>();

        public void Take(IPlayer player)
        {
            foreach (var playerEffect in playerEffects)
            {
                playerEffect.Take(player);
            }
        }

        [ContextMenu(nameof(AddHealEffect))]
        public void AddHealEffect() => playerEffects.Add(new HealEffect());

        [ContextMenu(nameof(AddDamageEffect))]
        public void AddDamageEffect() => playerEffects.Add(new DamageEffect());
    }
}
