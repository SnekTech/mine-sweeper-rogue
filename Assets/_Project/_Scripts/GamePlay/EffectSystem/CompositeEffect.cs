using System.Collections.Generic;
using UnityEngine;

namespace SnekTech.GamePlay.EffectSystem
{
    public class CompositeEffect<T> : ScriptableObject, IEffect<T>
    {
        [SerializeReference]
        private List<IEffect<T>> effects;


        public void Take(T target)
        {
            foreach (var effect in effects)
            {
                effect.Take(target);
            }
        }
    }
}
