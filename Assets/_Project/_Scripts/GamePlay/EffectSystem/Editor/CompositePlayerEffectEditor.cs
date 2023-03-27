using SnekTech.GamePlay.EffectSystem.PlayerEffects;
using SnekTech.GamePlay.PlayerSystem;
using UnityEditor;

namespace SnekTech.GamePlay.EffectSystem
{
    [CustomEditor(typeof(CompositePlayerEffect))]
    public class CompositePlayerEffectEditor : CompositeEffectEditor<IPlayer, IPlayerEffect>
    {
    }
}