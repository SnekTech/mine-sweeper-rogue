using System;
using UnityEngine;

namespace SnekTech.GamePlay.EffectSystem
{
    public abstract class FiniteEffect<T> : Effect<T>
    {
        [Min(0)]
        [SerializeField]
        private int repeatTimes;

        public int RepeatTimesRuntime { get; set; }

        public bool IsActive => RepeatTimesRuntime > 0;
        
        protected abstract Effect<T> DecoratedEffect { get; }

        public void Init()
        {
            RepeatTimesRuntime = repeatTimes;
        }

        public override void Take(T target)
        {
            if (RepeatTimesRuntime <= 0) return;
            
            DecoratedEffect.Take(target);

            RepeatTimesRuntime--;
        }
    }
}
