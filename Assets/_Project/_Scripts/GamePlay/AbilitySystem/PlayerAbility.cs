using SnekTech.C;
using SnekTech.GamePlay.EffectSystem;
using SnekTech.GamePlay.EffectSystem.PlayerEffects;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.AbilitySystem
{
    [CreateAssetMenu(menuName = MenuName.Abilities + "/" + nameof(PlayerAbility))]
    public class PlayerAbility : Ability<IPlayer>
    {
        [SerializeField]
        private CompositePlayerEffect effect;

        protected override IEffect<IPlayer> Effect => effect;

        private void OnEnable()
        {
            PlayerAbilityRepo.Instance.Set(name, this);
        }
    }
}
