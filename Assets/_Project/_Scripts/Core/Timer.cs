using System;

namespace SnekTech.Core
{
    public class Timer
    {
        public event Action TimedOut;
        
        private float _time;
        private float _duration;
        private bool _isTicking;
        private bool _hasTimedOut;

        public bool HasTimedOut => _hasTimedOut;
        public float CurrentTime => _time;
        public float Duration => _duration;

        public Timer()
        {
            Reset();
        }

        public void Reset()
        {
            _time = 0;
            _duration = 0;
            _isTicking = false;
            _hasTimedOut = false;
        }
        
        public void StartCountDown(float duration)
        {
            Reset();
            _isTicking = true;
            _duration = duration;
        }

        public void Tick(float deltaTime)
        {
            if (!_isTicking || _hasTimedOut)
            {
                return;
            }
            
            _time += deltaTime;

            if (_time > _duration)
            {
                _hasTimedOut = true;
                TimedOut?.Invoke();
            }
        }
    }
}
