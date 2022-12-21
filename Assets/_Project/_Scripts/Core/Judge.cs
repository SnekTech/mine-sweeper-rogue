using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekTech.Core.GameModeSystem;
using SnekTech.Core.History;
using SnekTech.DataPersistence;
using SnekTech.Grid;
using SnekTech.InventorySystem;
using SnekTech.Player;
using SnekTech.SceneManagement;
using SnekTech.UI;
using SnekTech.UI.ChooseItem;
using SnekTech.UI.Modal;
using UnityEngine;

namespace SnekTech.Core
{
    [RequireComponent(typeof(RecordHolder))]
    public class Judge : MonoBehaviour
    {
        [Header("DI")]
        [SerializeField]
        private GridEventManager gridEventManager;
        [SerializeField]
        private MySceneManager mySceneManager;
        [SerializeField]
        private PlayerState playerState;
        [SerializeField]
        private DataPersistenceManager dataPersistenceManager;

        [Header("Levels")]
        [SerializeField]
        private GridBehaviour grid;
        [SerializeField]
        private GridDataPool gridDataPool;

        [Header("Choose Item")]
        [SerializeField]
        private ModalManager modalManager;
        [SerializeField]
        private UIEventManager uiEventManager;
        [SerializeField]
        private ChooseItemPanel chooseItemPanelPrefab;
        
        [Header("Game Mode")]
        [SerializeField]
        private CountDownText countDownText;
        
        private GameMode _currentGameMode;

        private List<GameMode> _availableGameModes;
        private RecordHolder _recordHolder;


        private const int LevelCount = 3;
        private int _currentLevelIndex;
        private List<Level> _levels;

        private void Awake()
        {
            _recordHolder = GetComponent<RecordHolder>();
            
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
            mySceneManager.GameSceneLoaded += OnGameSceneLoaded;
        }

        private void OnDisable()
        {
            mySceneManager.GameSceneLoaded -= OnGameSceneLoaded;
        }

        private void OnGameSceneLoaded()
        {
            _recordHolder.Init();
            GenerateLevels();

            _currentLevelIndex = 0;
            LoadLevel(_currentLevelIndex);
        }
        
        private void LoadLevel(int levelIndex)
        {
            dataPersistenceManager.SaveGame();
            
            Level currentLevel = _levels[levelIndex];
            grid.InitCells(currentLevel.GridData);
            _currentGameMode = currentLevel.GameMode;
            
            _currentGameMode.Start();
            _currentGameMode.LevelCompleted += OnLevelCompleted;
        }

        private async UniTaskVoid StopGame(bool hasFailed)
        {
            
            _recordHolder.StoreCurrentRecord(hasFailed);
            
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

        private async void OnLevelCompleted(bool hasFailed)
        {
            _currentLevelIndex++;
            dataPersistenceManager.SaveGame();
            
            _currentGameMode.Stop();
            _currentGameMode.LevelCompleted -= OnLevelCompleted;

            if (hasFailed || _currentLevelIndex >= LevelCount)
            {
                StopGame(hasFailed).Forget();
            }
            else
            {
                
                ChooseItemPanel chooseItemPanel = Instantiate(chooseItemPanelPrefab);

                await modalManager.Show(new ModalContent(ChooseItemPanel.HeaderText, chooseItemPanel.gameObject));

                // close modal when item picked
                async void OnItemPicked(ItemData itemData)
                {
                    await modalManager.Hide();
                    uiEventManager.ItemChosen -= OnItemPicked;
                    
                    LoadLevel(_currentLevelIndex);
                }

                uiEventManager.ItemChosen += OnItemPicked;
            }
        }

        private void GenerateLevels()
        {
            _levels = new List<Level>(LevelCount);
            for (int i = 0; i < LevelCount; i++)
            {
                _levels.Add(new Level(gridDataPool.GetRandom(), ChooseGameMode()));
            }
        }

        private GameMode ChooseGameMode()
        {
            // todo: random set game mode?
            return _availableGameModes[0];
        }
    }
}