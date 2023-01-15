using System.Collections.Generic;
using SnekTech.Core.CustomAttributes;
using UnityEngine;

namespace SnekTech.Roguelike
{
    public abstract class RandomPool<T> : ScriptableObject, IRandomPool<T> where T : ScriptableObject
    {
        [PoolElementsField]
        [SerializeField]
        protected List<T> elements;

        public virtual T GetRandom() => elements.GetRandom();
    }
}