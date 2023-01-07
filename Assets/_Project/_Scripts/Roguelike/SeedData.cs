using System;
using UnityEngine;

namespace SnekTech.Roguelike
{
    [Serializable]
    public class SeedData
    {
        [SerializeField]
        private string seed;

        [SerializeField]
        private int generationCount;

        public string Seed
        {
            get => seed;
            set => seed = value;
        }

        public int GenerationCount
        {
            get => generationCount;
            set => generationCount = value;
        }

        public SeedData()
            : this(Guid.NewGuid().ToShortString(), 0)
        {
        }

        public SeedData(string seed, int generationCount)
        {
            this.seed = seed;
            this.generationCount = generationCount;
        }
    }
}
