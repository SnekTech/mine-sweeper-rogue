using System;
using System.Collections.Generic;
using System.Linq;
using SnekTech.Editor;
using UnityEditor;
using UnityEditor.Callbacks;

namespace SnekTech.GamePlay.EffectSystem.Editor
{
    [CustomEditor(typeof(PlayerFiniteEffect))]
    public class PlayerFiniteEffectEditor : FiniteEffectEditor
    {
        private static readonly List<Type> EffectTypes = new List<Type>();

        protected override List<Type> DecoratedEffectTypeCandidates => EffectTypes;

        [DidReloadScripts]
        public static void UpdateEffectTypes()
        {
            EffectTypes.Clear();
            EffectTypes.AddRange(ReflectionUtils.GetAllSubClassesOf<PlayerEffect>()
                .Where(type => type != typeof(PlayerFiniteEffect)));
        }
    }
}
