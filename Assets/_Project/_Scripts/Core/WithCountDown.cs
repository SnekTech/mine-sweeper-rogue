
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SnekTech.Core
{
    public class WithCountDown : GameMode
    {
        private const float CountDownIntervalSeconds = 0.1f;
        
        private float _durationSeconds;
        private readonly GameMode _decoratedMode;
        private readonly ICountDownDisplay _countDownDisplay;

        public WithCountDown(GameMode decoratedMode, float durationSeconds, ICountDownDisplay countDownDisplay = null) : base(decoratedMode.PlayerData)
        {
            _decoratedMode = decoratedMode;
            _durationSeconds = durationSeconds;
            _countDownDisplay = countDownDisplay;
        }

        protected override void OnStart()
        {
            _decoratedMode.Start();
            _decoratedMode.LevelCompleted += OnDecoratedModeLevelCompleted;
            
            StartTimerAsync().Forget();
        }

        protected override void OnStop()
        {
            _decoratedMode.Stop();
            _decoratedMode.LevelCompleted -= OnDecoratedModeLevelCompleted;
        }

        private void OnDecoratedModeLevelCompleted(bool hasFailed)
        {
            InvokeLevelCompleted(hasFailed);
        }

        private async UniTaskVoid StartTimerAsync()
        {
           _countDownDisplay?.SetActive(true); 
            
            while (_durationSeconds > 0)
            {
                _countDownDisplay?.UpdateDurationRemaining(_durationSeconds);
                await UniTask.Delay(TimeSpan.FromSeconds(CountDownIntervalSeconds));
                _durationSeconds -= CountDownIntervalSeconds;
            }
            _countDownDisplay?.SetActive(false);
            
            InvokeLevelCompleted(true);
        }
    }
}
