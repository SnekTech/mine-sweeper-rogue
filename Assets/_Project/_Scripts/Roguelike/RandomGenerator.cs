using System;
using System.Collections.Generic;
using SnekTech.DataPersistence;
using UnityEngine;
using Random = System.Random;

namespace SnekTech.Roguelike
{
    public sealed class RandomGenerator : IRandomGenerator, IPersistentDataHolder
    {
        #region Singleton

        private static readonly RandomGenerator instance = new RandomGenerator();
                
        static RandomGenerator(){}

        private RandomGenerator()
        {
        }
        
        public static RandomGenerator Instance => instance;

        #endregion
        
        private Random _random;
        private string _seedString = "";

        public string Seed => _seedString;
        
        public int GenerationCount { get; private set; }

        public void Init(string seed)
        {
            _seedString = seed;
            _random = new Random(seed.GetHashCode());
            GenerationCount = 0;
        }

        private void Init(string seed, int generationCount)
        {
            Init(seed);
            for (int i = 0; i < generationCount; i++)
            {
                Next();
            }
        }

        #region Generation Functions

        public int Next()
        {
            GenerationCount++;
            return _random.Next();
        }

        public int Next(int maxValue) => Next(0, maxValue);

        public int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException("minValue should be less than or equal to maxValue");
            }

            float proportion = Next() / (float) int.MaxValue;
            return (int) (minValue + (maxValue - minValue) * proportion);
        }

        public bool NextBool(float probability = 0.5f)
        {
            if (probability is < 0 or > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(probability));
            }

            float threshold = int.MaxValue * probability;

            return Next() < threshold;
        }

        #endregion

        #region Data Save & Load

        public void LoadData(GameData gameData)
        {
            SeedData seedData = gameData.seedData;
            Init(seedData.Seed, seedData.GenerationCount);
        }

        public void SaveData(GameData gameData)
        {
            gameData.seedData = new SeedData(Seed, GenerationCount);
        }
        
        #endregion
    }
}