﻿using System;
using SnekTech.Player;
using UnityEngine;

namespace SnekTech.Core.History
{
    public class RecordHolder : MonoBehaviour
    {
        [SerializeField]
        private GameHistory history;

        [SerializeField]
        private PlayerState playerState;
        
        private Record _currentRecord;

        public void Init()
        {
            _currentRecord = new Record();
        }

        public void StoreCurrentRecord(bool hasFailed)
        {
            _currentRecord.SetCreatedAt(DateTime.UtcNow.Ticks);
            _currentRecord.SetItems(playerState.Inventory.Items);
            _currentRecord.SetCellEvents(playerState.GameEventHolder.CellEvents);
            _currentRecord.SetResult(hasFailed);
            
            history.AddRecord(_currentRecord);
        }
    }
}