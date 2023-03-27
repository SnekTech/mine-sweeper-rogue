using SnekTech.C;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.EffectSystem.PlayerEffects
{
    [CreateAssetMenu(menuName = MenuName.Effects + "/" + nameof(CompositePlayerEffect))]
    public class CompositePlayerEffect : CompositeEffect<IPlayer, IPlayerEffect>
    {
    }
}