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

        public Sprite Icon => icon;
        public string Label => label;
        public string Description => description;
        
        public abstract bool IsActive { get; }
        public virtual void Init() {}
        public abstract void Use(T target);
    }
}
