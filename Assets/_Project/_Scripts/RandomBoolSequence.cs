using System;

namespace SnekTech
{
    public class RandomBoolSequence : ISequence<bool>
    {
        private readonly int _seed;
        private Random _random;

        public RandomBoolSequence(int seed)
        {
            _seed = seed;
            Reset();
        }

        public void Reset()
        {
            _random = new Random(_seed);
        }

        public bool Next()
        {
            return _random.Next(0, 2) == 1;
        }
    }
}
