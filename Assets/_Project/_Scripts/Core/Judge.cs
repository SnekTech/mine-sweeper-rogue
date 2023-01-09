using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekTech.Core.GameModeSystem;
using SnekTech.Core.History;
using SnekTech.DataPersistence;
using SnekTech.Grid;
using SnekTech.Player;
using SnekTech.SceneManagement;
using SnekTech.UI.Modal;
using UnityEngine;

namespace SnekTech.Core
{
    public class Judge : MonoBehaviour
    {
        public event Action OnLevelLoad;
        
        [Header("DI")]
        [SerializeField]
        private GridEventManager gridEventManager;
        [SerializeField]
        private MySceneManager mySceneManager;
        [SerializeField]
        private PlayerState playerState;
        [SerializeField]
        private DataPersistenceManager dataPersistenceManager;
        [SerializeField]
        private CurrentRecordHolder currentRecordHolder;

        [Header("Levels")]
        [SerializeField]
        private GridBehaviour grid;
        [SerializeField]
        private GridDataPool gridDataPool;

        [Header("Choose Item")]
        [SerializeField]
        private ModalManager modalManager;

        [Header("Game Mode")]
        [SerializeField]
        private GameModeInfo classicModeInfo;
        [SerializeField]
        private GameModeInfo countDownModeInfo;
        [SerializeField]
        private CountDownText countDownText;
        
        private List<GameMode> _availableGameModes;

        private List<IShouldFinishAfterLevelCompleted> _afterLevelCompletedTasks;

        private const int LevelCount = 3;
        public Level CurrentLevel { get; private set; }
        private GameMode CurrentGameMode => CurrentLevel.GameMode;
        private IGrid Grid => grid;

        private int CurrentLevelIndex
        {
            get => currentRecordHolder.CurrentLevelIndex;
            set => currentRecordHolder.CurrentLevelIndex = value;
        }
        
        #region Unity callbacks

        private void Awake()
        {
            var classicMode = new ClassicMode(gridEventManager, classicModeInfo, playerState);
            var countDownMode = new WithCountDown(countDownModeInfo, classicMode, WithCountDown.DefaultDuration, countDownText);
            
            _availableGameModes = new List<GameMode>
            {
                classicMode,
                countDownMode,
            };

            _afterLevelCompletedTasks = new List<IShouldFinishAfterLevelCompleted>
            {
                modalManager,
            };
        }
        
        private void OnEnable()
        {
            mySceneManager.GameSceneLoaded += OnGameSceneLoaded;
        }

        private void OnDisable()
        {
            mySceneManager.GameSceneLoaded -= OnGameSceneLoaded;
        }

        #endregion
        
        private void OnGameSceneLoaded()
        {
            LoadLevel(GenerateLevel(CurrentLevelIndex));
        }
        
        private void LoadLevel(Level level)
        {
            CurrentLevel = level;
            OnLevelLoad?.Invoke();
            
            Grid.InitCells(level.GridData);
            
            CurrentGameMode.Start();
            CurrentGameMode.OnLevelComplete += HandleOnLevelComplete;
            
            dataPersistenceManager.SaveGame();
        }

        private async UniTaskVoid StopGame(bool hasFailed)
        {
            
            currentRecordHolder.StoreCurrentRecord(hasFailed);
            dataPersistenceManager.SaveGame();
            
            if (hasFailed)
            {
                await mySceneManager.LoadSceneAsync(SceneIndex.GameOver);
            }
            else
            {
                // todo: load winning scene
                Debug.Log("You won!");
            }
        }

        private void HandleOnLevelComplete(bool hasFailed) => HandleOnLevelCompleteAsync(hasFailed).Forget();

        private async UniTaskVoid HandleOnLevelCompleteAsync(bool hasFailed)
        {
            await UniTask.WhenAll(_afterLevelCompletedTasks.Select(task => task.FinishAsync()));
            
            CurrentLevelIndex++;
            playerState.ClearAllEffects();
            dataPersistenceManager.SaveGame();
            
            
            CurrentGameMode.Stop();
            CurrentGameMode.OnLevelComplete -= HandleOnLevelComplete;

            if (hasFailed || CurrentLevelIndex >= LevelCount)
            {
                StopGame(hasFailed).Forget();
            }
            else
            {
                await modalManager.ShowChooseItemPanelAsync(
                    () => LoadLevel(GenerateLevel(CurrentLevelIndex)));
            }
        }

        private Level GenerateLevel(int levelIndex)
        {
            return new Level(gridDataPool.GetRandom(), ChooseGameMode(), levelIndex);
        }

        private GameMode ChooseGameMode()
        {
            // todo: random set game mode?
            return _availableGameModes[0];
        }
    }
}