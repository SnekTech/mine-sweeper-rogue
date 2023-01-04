using System;
using SnekTech.Player.OneTimeEffect;

namespace SnekTech.Player.ClickEffect
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

        public void Take(PlayerState playerState)
        {
            if (!IsValidRepeatTime(RepeatTime))
            {
                return;
            }
            
            _decoratedClickEffect.Take(playerState);
            RepeatTime--;
        }

        private static bool IsValidRepeatTime(int repeatTime)
        {
            return repeatTime > 0;
        }
    }
}
