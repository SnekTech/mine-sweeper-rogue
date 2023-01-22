using UnityEngine;

namespace SnekTech.GamePlay.EffectSystem
{
    public abstract class FiniteEffect<T> : ScriptableObject, IEffect<T>
    {
        [Min(0)]
        [SerializeField]
        private int repeatTimes;

        public int RepeatTimes
        {
            get => repeatTimes;
            set => repeatTimes = value;
        }

        public bool IsActive => repeatTimes > 0;
        
        protected abstract IEffect<T> DecoratedEffect { get; }

        public void Take(T target)
        {
            if (repeatTimes <= 0) return;
            
            DecoratedEffect.Take(target);

            repeatTimes--;
        }
    }
}
