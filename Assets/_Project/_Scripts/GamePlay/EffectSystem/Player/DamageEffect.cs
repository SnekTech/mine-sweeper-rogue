using System;
using SnekTech.Editor.CustomAttributes;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.EffectSystem
{
    [CreateAssetMenu(menuName = C.MenuName.Effects + "/" + nameof(DamageEffect))]
    public class DamageEffect : PlayerEffect
    {
        [EffectField]
        [Min(0)]
        [SerializeField]
        private int amount = 3;
        
        public override void Take(IPlayer player)
        {
            player.TakeDamage(amount);
        }
    }
}
