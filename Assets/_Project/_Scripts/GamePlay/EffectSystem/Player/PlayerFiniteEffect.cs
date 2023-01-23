using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.EffectSystem
{
    [CreateAssetMenu(menuName = C.MenuName.Effects + "/" + nameof(PlayerFiniteEffect))]
    public class PlayerFiniteEffect : FiniteEffect<IPlayer>
    {

        [SerializeReference]
        private PlayerEffect decoratedEffect;

        protected override Effect<IPlayer> DecoratedEffect => decoratedEffect;
    }
}
