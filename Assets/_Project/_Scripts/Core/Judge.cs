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
        private PlayerData playerData;
        
        private GameMode _currentGameMode;

        private Timer _timer;

        private List<GameMode> _availableGameModes;

        private void Awake()
        {
            _timer = new Timer();
            
            var classicMode = new ClassicMode(gridEventManager, playerData);
            var countDownMode = new WithCountDown(classicMode, _timer);
            
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

        private void Update()
        {
            _timer.Tick(Time.deltaTime);
        }
        
        private void StartGame()
        {
            _currentGameMode.Start();
            _currentGameMode.LevelCompleted += OnLevelCompleted;
        }

        private async UniTask StopGame(bool hasFailed)
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

        private async void OnLevelCompleted(bool hasFailed)
        {
            await StopGame(hasFailed);
        }

        private GameMode ChooseGameMode()
        {
            // todo: random set game mode?
            return _availableGameModes[1];
        }
    }
}
