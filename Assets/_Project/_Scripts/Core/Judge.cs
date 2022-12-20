using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekTech.Core.History;
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

        [Header("Levels")]
        [SerializeField]
        private GridBehaviour grid;
        [SerializeField]
        private GridData gridData; // todo: replace with level data

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
        
        private void StartGame()
        {
            _currentGameMode.Start();
            _currentGameMode.LevelCompleted += OnLevelCompleted;
        }

        private async UniTaskVoid StopGame(bool hasFailed)
        {
            _currentGameMode.Stop();
            _currentGameMode.LevelCompleted -= OnLevelCompleted;
            
            _recordHolder.StoreCurrentRecord(hasFailed);
            
            if (hasFailed)
            {
                await mySceneManager.LoadSceneAsync(SceneIndex.GameOver);
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
                    
                    // todo: load next level
                    Debug.Log("time to load next level");
                }

                uiEventManager.ItemChosen += OnItemPicked;
            }
        }

        private void OnGameSceneLoaded()
        {
            _currentGameMode = ChooseGameMode();
            _recordHolder.Init();
            
            grid.InitCells(gridData);
            
            StartGame();
        }

        private void OnLevelCompleted(bool hasFailed)
        {
            // bug: level completion shouldn't stop game, if there are levels remaining
            StopGame(hasFailed).Forget();
        }

        private GameMode ChooseGameMode()
        {
            // todo: random set game mode?
            return _availableGameModes[0];
        }
    }
}