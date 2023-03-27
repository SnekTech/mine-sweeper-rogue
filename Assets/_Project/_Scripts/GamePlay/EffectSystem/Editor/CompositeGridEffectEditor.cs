using SnekTech.GamePlay.EffectSystem.GridEffects;
using SnekTech.MineSweeperRogue.GridSystem;
using UnityEditor;

namespace SnekTech.GamePlay.EffectSystem
{
    [CustomEditor(typeof(CompositeGridEffect))]
    public class CompositeGridEffectEditor : CompositeEffectEditor<IGrid, IGridEffect>
    {
    }
}