using System;
using SnekTech.Core.GameEvent;
using SnekTech.Player;
using SnekTech.Roguelike;
using UnityEngine;

namespace SnekTech.Core.History
{
    public class RecordHolder : MonoBehaviour
    {
        [SerializeField]
        private GameHistory history;

        [SerializeField]
        private PlayerState playerState;

        [SerializeField]
        private GameEventHolder gameEventHolder;
        
        private Record _currentRecord;
        private readonly IRandomGenerator _randomGenerator = RandomGenerator.Instance;

        public void Init()
        {
            _currentRecord = new Record();
        }

        public void StoreCurrentRecord(bool hasFailed)
        {
            _currentRecord.SetCreatedAt(DateTime.UtcNow.Ticks);
            _currentRecord.SetItems(playerState.Inventory.Items);
            _currentRecord.SetCellEvents(gameEventHolder.CellEvents);
            _currentRecord.SetResult(hasFailed);
            _currentRecord.Seed = _randomGenerator.Seed;
            
            history.AddRecord(_currentRecord);
        }
    }
}