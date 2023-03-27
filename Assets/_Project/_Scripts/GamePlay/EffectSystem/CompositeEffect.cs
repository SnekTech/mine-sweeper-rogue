using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SnekTech.GamePlay.EffectSystem
{
    /// <summary>
    ///     
    /// </summary>
    /// <typeparam name="T">target type, player or grid</typeparam>
    /// <typeparam name="TEffect">effect type, because generic SerializedReference is not supported</typeparam>
    public abstract class CompositeEffect<T, TEffect> : ScriptableObject, IEffect<T>
        where TEffect : IEffect<T>
    {
        [SerializeReference]
        private List<TEffect> effects = new List<TEffect>();

        public async UniTask Take(T target)
        {
            var takeEffectTasks = effects.Select(e => e.Take(target));
            await UniTask.WhenAll(takeEffectTasks);
        }
    }
}