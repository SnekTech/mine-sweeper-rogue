using SnekTech.GamePlay.EffectSystem;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.AbilitySystem
{
    [CreateAssetMenu(menuName = C.MenuName.Abilities + "/" + nameof(HealAbility))]
    public class HealAbility : PlayerAbility
    {
        [SerializeField]
        private HealEffect healEffect;
        
        public override void Use(IPlayer player)
        {
            healEffect.Take(player);
        }
    }
}
