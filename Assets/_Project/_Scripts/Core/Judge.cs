using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekTech.Grid;
using SnekTech.Player;
using SnekTech.SceneManagement;
using UnityEngine;

namespace SnekTech.Core
{
    public class Judge : MonoBehaviour
    {
        [SerializeField]
        private GridEventManager gridEventManager;
        [SerializeField]
        private PlayerState playerState;
        
        [SerializeField]
        private CountDownText countDownText;
        
        private GameMode _currentGameMode;

        private List<GameMode> _availableGameModes;

        private void Awake()
        {
            var classicMode = new ClassicMode(gridEventManager, playerState);
            var countDownMode = new WithCountDown(classicMode, Constants.GameConstants.DefaultCountDownDuration, countDownText);
            
            _availableGameModes = new List<GameMode>
            {
                classicMode,
                countDownMode,
            };

        }

        private void OnEnable()
        {
            gridEventManager.GridInitCompleted += OnGridInitCompleted;
        }

        private void OnDisable()
        {
            gridEventManager.GridInitCompleted -= OnGridInitCompleted;
        }
        
        private void StartGame()
        {
            _currentGameMode.Start();
            _currentGameMode.LevelCompleted += OnLevelCompleted;
        }

        private async UniTaskVoid StopGame(bool hasFailed)
        {
            _currentGameMode.Stop();
            _currentGameMode.LevelCompleted -= OnLevelCompleted;
            
            if (hasFailed)
            {
                await MySceneManager.LoadSceneAsync(SceneIndex.GameOver);
            }
            else
            {
                // todo: load next level or win the game
                Debug.Log("level passed");
            }
        }

        private void OnGridInitCompleted(IGrid obj)
        {
            _currentGameMode = ChooseGameMode();
            StartGame();
        }

        private void OnLevelCompleted(bool hasFailed)
        {
            StopGame(hasFailed).Forget();
        }

        private GameMode ChooseGameMode()
        {
            // todo: random set game mode?
            return _availableGameModes[0];
        }
    }
}
