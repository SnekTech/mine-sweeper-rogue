using System;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.EffectSystem
{
    [Serializable]
    public class HealEffect : IPlayerEffect
    {
        [SerializeField]
        private int amount;
        
        public void Take(IPlayer player)
        {
            player.AddHealth(amount);
        }
    }
}
