using System.Collections.Generic;
using System.Linq;
using SnekTech.DataPersistence;
using UnityEngine;

namespace SnekTech.Core.History
{
    [CreateAssetMenu(menuName = C.MenuName.GameHistory + "/" + nameof(GameHistory), fileName = nameof(GameHistory))]
    public class GameHistory : ScriptableObject, IPersistentDataHolder
    {
        private HistoryData _historyData;
        private readonly SortedSet<Record> _sortedRecords = new SortedSet<Record>();

        public List<Record> Records => _sortedRecords.ToList();

        public void AddRecord(Record record)
        {
            _sortedRecords.Add(record);
        }

        private void LoadRecordsToSortedSet(List<Record> records)
        {
            _sortedRecords.Clear();
            foreach (Record record in records)
            {
                _sortedRecords.Add(record);
            }
        }

        public void LoadData(GameData gameData)
        {
            _historyData = gameData.historyData;
            
            LoadRecordsToSortedSet(_historyData.records);
        }

        public void SaveData(GameData gameData)
        {
            _historyData.records = Records;
            
            gameData.historyData = _historyData;
        }
    }
}
