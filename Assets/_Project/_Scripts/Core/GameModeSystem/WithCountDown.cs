using System;
using Cysharp.Threading.Tasks;

namespace SnekTech.Core.GameModeSystem
{
    public interface ICountDownDisplay : ICanChangeActiveness
    {
        void UpdateDurationRemaining(float durationRemaining);
    }
    
    public class WithCountDown : GameMode
    {
        private const float CountDownIntervalSeconds = 0.1f;
        public const float DefaultDuration = 3f;// todo: randomize this across different levels
        
        private float _durationSeconds;
        private readonly GameMode _decoratedMode;
        private readonly ICountDownDisplay _display;

        public WithCountDown(GameModeInfo gameModeInfo, GameMode decoratedMode, float durationSeconds,
            ICountDownDisplay countDownDisplay = null) : base(gameModeInfo, decoratedMode.PlayerState)
        {
            _decoratedMode = decoratedMode;
            _durationSeconds = durationSeconds;
            _display = countDownDisplay;
        }

        protected override void OnStart()
        {
            _decoratedMode.Start();
            _decoratedMode.OnLevelComplete += HandleDecoratedModeOnLevelComplete;
            
            StartTimerAsync().Forget();
        }

        protected override void OnStop()
        {
            _decoratedMode.Stop();
            _decoratedMode.OnLevelComplete -= HandleDecoratedModeOnLevelComplete;
        }

        private void HandleDecoratedModeOnLevelComplete(bool hasFailed)
        {
            InvokeLevelCompleted(hasFailed);
        }

        private async UniTaskVoid StartTimerAsync()
        {
            SetDisplayActive(true);
            
            while (_durationSeconds > 0)
            {
                _display?.UpdateDurationRemaining(_durationSeconds);
                await UniTask.Delay(TimeSpan.FromSeconds(CountDownIntervalSeconds));
                _durationSeconds -= CountDownIntervalSeconds;
            }
            
            SetDisplayActive(false);
            InvokeLevelCompleted(true);
        }

        private void SetDisplayActive(bool isActive)
        {
            if (_display != null)
            {
                _display.IsActive = isActive;
            }
        }
    }
}
