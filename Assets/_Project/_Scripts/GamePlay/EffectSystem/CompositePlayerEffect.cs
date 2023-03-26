using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekTech.GamePlay.PlayerSystem;
using UnityEngine;

namespace SnekTech.GamePlay.EffectSystem
{
    [CreateAssetMenu]
    public class CompositePlayerEffect : ScriptableObject, IPlayerEffect
    {
        [SerializeReference]
        private List<IPlayerEffect> effects;

        public async UniTask Take(IPlayer target)
        {
            var takeEffectTasks = effects.Select(e => e.Take(target));
            await UniTask.WhenAll(takeEffectTasks);
        }
    }
}