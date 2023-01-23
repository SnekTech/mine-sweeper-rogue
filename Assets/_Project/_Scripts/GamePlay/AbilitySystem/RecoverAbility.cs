using SnekTech.GamePlay.EffectSystem;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.AbilitySystem
{
    [CreateAssetMenu(menuName = C.MenuName.Abilities + "/" + nameof(RecoverAbility))]
    public class RecoverAbility : PlayerAbility
    {
        [SerializeField]
        private PlayerFiniteEffect finiteHealEffect;

        public override bool IsActive => finiteHealEffect.IsActive;

        public override void Init()
        {
            finiteHealEffect.Init();
        }

        public override void Use(IPlayer player)
        {
            finiteHealEffect.Take(player);
        }
    }
}
