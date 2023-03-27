using SnekTech.C;
using SnekTech.MineSweeperRogue.GridSystem;
using UnityEngine;

namespace SnekTech.GamePlay.EffectSystem.GridEffects
{
    [CreateAssetMenu(menuName = MenuName.Effects + "/" + nameof(CompositeGridEffect))]
    public class CompositeGridEffect : CompositeEffect<IGrid, IGridEffect>
    {
    }
}
