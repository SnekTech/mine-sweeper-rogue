using System;
using System.Globalization;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace SnekTech.Core.GameModeSystem
{
    public class CountDownText : MonoBehaviour, ICountDownDisplay
    {
        [SerializeField]
        private TMP_Text text;

        public void UpdateDurationRemaining(float durationRemaining)
        {
            text.text = durationRemaining.ToString("F1", CultureInfo.InvariantCulture);
        }

        public void SetActive(bool isActive)
        {
            text.gameObject.SetActive(isActive);
        }
    }
    
    public interface ICountDownDisplay
    {
        void UpdateDurationRemaining(float durationRemaining);
        void SetActive(bool isActive);
    }
    
    public class WithCountDown : GameMode
    {
        private const float CountDownIntervalSeconds = 0.1f;
        
        private float _durationSeconds;
        private readonly GameMode _decoratedMode;
        private readonly ICountDownDisplay _countDownDisplay;

        public WithCountDown(GameMode decoratedMode, float durationSeconds, ICountDownDisplay countDownDisplay = null) : base(decoratedMode.PlayerState)
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
