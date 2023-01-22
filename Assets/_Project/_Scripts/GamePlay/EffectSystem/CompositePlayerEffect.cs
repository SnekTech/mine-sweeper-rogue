using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.EffectSystem
{
    [CreateAssetMenu(menuName = C.MenuName.Effects + "/" + nameof(CompositePlayerEffect))]
    public class CompositePlayerEffect : CompositeEffect<IPlayer>
    {
    }
}
