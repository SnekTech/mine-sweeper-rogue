using SnekTech.C;
using SnekTech.Editor.CustomAttributes;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.EffectSystem
{
    [CreateAssetMenu(menuName = MenuName.Effects + "/" + nameof(HealEffect))]
    public class HealEffect : PlayerEffect
    {
        [EffectField]
        [Min(0)]
        [SerializeField]
        private int amount;
        
        public override void Take(IPlayer player)
        {
            player.AddHealth(amount);
        }
    }
}
