using System;

namespace SnekTech
{
    public class RandomBombGenerator : ISequence<bool>
    {
        private readonly int _seed;
        private readonly float _threshHold;
        
        private Random _random;

        public RandomBombGenerator(int seed, float bombPercentage)
        {
            _seed = seed;
            _threshHold = int.MaxValue * bombPercentage;
            Reset();
        }

        public void Reset()
        {
            _random = new Random(_seed);
        }

        public bool Next()
        {
            return _random.Next() < _threshHold;
        }
    }
}
