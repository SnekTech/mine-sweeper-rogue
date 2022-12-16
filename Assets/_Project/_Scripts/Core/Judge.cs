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
    public class Judge : MonoBehaviour
    {
        [SerializeField]
        private GridEventManager gridEventManager;
        [SerializeField]
        private PlayerState playerState;
        [SerializeField]
        private MySceneManager mySceneManager;

        [SerializeField]
        private GameHistory gameHistory;

        [SerializeField]
        private ModalManager modalManager;
        [SerializeField]
        private UIEventManager uiEventManager;

        [SerializeField]
        private ChooseItemPanel chooseItemPanelPrefab;
        
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
            
            StoreCurrentRecord();
            
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

        private void StoreCurrentRecord()
        {
            playerState.CurrentRecord.SetCreatedAt(DateTime.UtcNow.Ticks);
            gameHistory.AddRecord(playerState.CurrentRecord);
        }
    }
}