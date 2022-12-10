using System;

namespace SnekTech.Roguelike
{
    public class RandomBoolSequence : IRandomSequence<bool>
    {
        private Random _random;
        private float _threshold;

        public RandomBoolSequence(int seed, float probability)
        {
            SetSeed(seed);
            SetProbability(probability);
        }
        
        public void SetSeed(int seed)
        {
            _random = new Random(seed);
        }

        public void SetProbability(float probability)
        {
            if (probability < 0 || probability > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(probability));
            }
            
            _threshold = int.MaxValue * probability;
        }

        public bool Next()
        {
            return _random.Next() < _threshold;
        }
    }
}
