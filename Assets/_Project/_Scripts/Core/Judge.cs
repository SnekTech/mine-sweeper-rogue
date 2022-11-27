using System;
using SnekTech.Grid;
using SnekTech.Player;
using UnityEngine;

namespace SnekTech.Core
{
    public class Judge : MonoBehaviour
    {
        [SerializeField]
        private GridEventManager gridEventManager;
        [SerializeField]
        private PlayerData playerData;
        
        private GameMode _gameMode;

        private Timer _timer;

        private void Awake()
        {
            _gameMode = new ClassicMode(gridEventManager, playerData);
            _timer = new Timer();
        }

        private void OnEnable()
        {
            gridEventManager.GridInitCompleted += OnGridInitCompleted;
            _gameMode.LevelCompleted += OnLevelCompleted;
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
            _gameMode.Start();
            _gameMode.LevelCompleted += OnLevelCompleted;
            
            _timer.StartCountDown(10); // todo: replace magic number
        }

        private void StopGame(bool hasFailed)
        {
            _gameMode.Stop();
            _gameMode.LevelCompleted -= OnLevelCompleted;
            
            if (hasFailed)
            {
                Debug.Log("level failed");
            }
            else
            {
                Debug.Log("level passed");
            }
        }

        private void OnGridInitCompleted(IGrid obj)
        {
            StartGame();
        }

        private void OnLevelCompleted(bool hasFailed)
        {
            StopGame(hasFailed);
        }

    }
}
