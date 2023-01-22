using System;
using SnekTech.Editor.CustomAttributes;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.EffectSystem
{
    [EffectObject]
    [Serializable]
    public class DamageEffect : IPlayerEffect
    {
        [EffectField]
        [Min(0)]
        [SerializeField]
        private int amount = 3;
        
        public void Take(IPlayer player)
        {
            player.TakeDamage(amount);
        }
    }
}
