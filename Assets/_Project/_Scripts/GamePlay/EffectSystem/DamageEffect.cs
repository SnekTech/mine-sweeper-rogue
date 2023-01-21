using System;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.EffectSystem
{
    [Serializable]
    public class DamageEffect : IPlayerEffect
    {
        [SerializeField]
        private int amount = 3;
        
        public void Take(IPlayer player)
        {
            player.TakeDamage(amount);
        }
    }
}
