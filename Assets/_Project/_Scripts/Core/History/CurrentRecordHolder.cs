using System;
using SnekTech.Core.GameEvent;
using SnekTech.DataPersistence;
using SnekTech.GamePlay;
using SnekTech.GamePlay.PlayerSystem;
using SnekTech.Roguelike;
using UnityEngine;

namespace SnekTech.Core.History
{
    [CreateAssetMenu(menuName = C.MenuName.GameHistory + "/" + nameof(CurrentRecordHolder))]
    public class CurrentRecordHolder : ScriptableObject, IPersistentDataHolder
    {
        #region Persistent Data

        public int CurrentLevelIndex { get; set; }

        #endregion
        
        #region Record Saving Dependencies

        private Player _player;
        private CurrentEventsHolder _currentEventsHolder;
        private GameHistory _history;
        private readonly IRandomGenerator _randomGenerator = RandomGenerator.Instance;

        #endregion

        public void Init(Player player, CurrentEventsHolder currentEventsHolder, GameHistory history)
        {
            _player = player;
            _currentEventsHolder = currentEventsHolder;
            _history = history;
        }

        public void StoreCurrentRecord(bool hasFailed)
        {
            var newRecord = new Record
            {
                CreatedAt = DateTime.UtcNow.Ticks,
                Items = _player.Inventory.Items,
                CellEvents = _currentEventsHolder.CellEvents,
                HasFailed = hasFailed,
                Seed = _randomGenerator.Seed,
            };
            
            _history.AddRecord(newRecord);
        }

        public void LoadData(GameData gameData)
        {
            CurrentLevelIndex = gameData.currentLevelIndex;
        }

        public void SaveData(GameData gameData)
        {
            gameData.currentLevelIndex = CurrentLevelIndex;
        }
    }
}