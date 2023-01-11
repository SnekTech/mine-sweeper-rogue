using System.Collections.Generic;
using UnityEngine;

namespace SnekTech.Roguelike
{
    public abstract class RandomPool<T> : ScriptableObject, IRandomPool<T> where T : ScriptableObject
    {
        [SerializeField]
        protected List<T> elements;
        
        public abstract string AssetDirPath { get; }

        public void Populate(List<T> newElements) => elements = newElements;

        public void Clear() => elements?.Clear();

        public virtual T GetRandom() => elements.GetRandom();
    }
}