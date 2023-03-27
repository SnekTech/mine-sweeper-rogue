using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.EffectSystem.PlayerEffects
{
    [CreateAssetMenu]
    public class CompositePlayerEffect : CompositeEffect<IPlayer, IPlayerEffect>
    {
    }
}