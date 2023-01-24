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

        [SerializeField]
        private Player player;
        [SerializeField]
        private CurrentEventsHolder currentEventsHolder;
        [SerializeField]
        private GameHistory history;
        private readonly IRandomGenerator _randomGenerator = RandomGenerator.Instance;

        #endregion

        public void StoreCurrentRecord(bool hasFailed)
        {
            var newRecord = new Record
            {
                CreatedAt = DateTime.UtcNow.Ticks,
                Items = player.Inventory.Items,
                CellEvents = currentEventsHolder.CellEvents,
                HasFailed = hasFailed,
                Seed = _randomGenerator.Seed,
            };
            
            history.AddRecord(newRecord);
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