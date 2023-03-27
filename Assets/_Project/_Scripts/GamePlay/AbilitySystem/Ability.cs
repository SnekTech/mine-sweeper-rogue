using Cysharp.Threading.Tasks;
using SnekTech.GamePlay.EffectSystem;
using UnityEngine;

namespace SnekTech.GamePlay.AbilitySystem
{
    public abstract class Ability<T> : ScriptableObject, IAbility<T>
    {
        [SerializeField]
        private Sprite icon;

        [SerializeField]
        private string label;

        [SerializeField]
        private string description;

        public int RepeatTimes { get; set; }
        
        public Sprite Icon => icon;
        public string Label => label;
        public string Description => $"{description} 剩余 {RepeatTimes} 次";

        protected abstract IEffect<T> Effect { get; }

        public bool IsActive => RepeatTimes > 0;

        public async UniTask Use(T target)
        {
            if (RepeatTimes <= 0)
                return;

            await Effect.Take(target);
            RepeatTimes--;
        }
    }
}
