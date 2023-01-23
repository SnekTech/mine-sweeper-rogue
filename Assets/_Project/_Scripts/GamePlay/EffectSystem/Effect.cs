using UnityEngine;

namespace SnekTech.GamePlay.EffectSystem
{
    public abstract class Effect<T> : ScriptableObject, IEffect<T>
    {
        public abstract void Take(T target);
    }
}
