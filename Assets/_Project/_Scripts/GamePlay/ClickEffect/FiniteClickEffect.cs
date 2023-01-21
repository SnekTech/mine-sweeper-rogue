using System;
using SnekTech.GamePlay.OneTimeEffect;
using SnekTech.GamePlay.PlayerSystem;

namespace SnekTech.GamePlay.ClickEffect
{
    public class FiniteClickEffect
    {
        private readonly IOneTimeEffect _decoratedClickEffect;

        public int RepeatTime { get; set; }
        public bool IsActive => IsValidRepeatTime(RepeatTime);
        
        public FiniteClickEffect(int repeatTime, IOneTimeEffect decoratedClickEffect)
        {
            if (!IsValidRepeatTime(repeatTime))
            {
                throw new ArgumentException("repeatTimes param should > 0");
            }
            RepeatTime = repeatTime;
            _decoratedClickEffect = decoratedClickEffect;
        }

        public void Take(Player player)
        {
            if (!IsValidRepeatTime(RepeatTime))
            {
                return;
            }
            
            _decoratedClickEffect.Take(player);
            RepeatTime--;
        }

        private static bool IsValidRepeatTime(int repeatTime)
        {
            return repeatTime > 0;
        }
    }
}
