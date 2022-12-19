using System;

namespace SnekTech.Player.ClickEffect
{
    public class FiniteClickEffect : IClickEffect
    {
        private int _repeatTime;
        private readonly IClickEffect _decoratedClickEffect;
        
        public FiniteClickEffect(int repeatTime, IClickEffect decoratedClickEffect)
        {
            if (!IsValidRepeatTime(repeatTime))
            {
                throw new InvalidOperationException("repeatTimes param should > 0");
            }
            _repeatTime = repeatTime;
            _decoratedClickEffect = decoratedClickEffect;
        }

        public void Take(PlayerState playerState)
        {
            if (!IsValidRepeatTime(_repeatTime))
            {
                return;
            }
            
            _decoratedClickEffect.Take(playerState);
            _repeatTime--;

            if (!IsValidRepeatTime(_repeatTime))
            {
                playerState.RemoveClickEffect(this);
            }
        }

        private static bool IsValidRepeatTime(int repeatTime)
        {
            return repeatTime > 0;
        }
    }
}
