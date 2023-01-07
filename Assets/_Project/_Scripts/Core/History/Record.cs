using System;
using System.Collections.Generic;
using SnekTech.Core.GameEvent;
using SnekTech.InventorySystem;
using UnityEngine;

namespace SnekTech.Core.History
{
    [Serializable]
    public class Record : IComparable<Record>
    {
        [SerializeField]
        private string seed;
        [SerializeField]
        private long createdAt;
        [SerializeField]
        private bool hasFailed;

        [SerializeField]
        private List<InventoryItem> items;
        [SerializeField]
        private List<CellEvent> cellEvents;


        public string Seed
        {
            get => seed;
            set => seed = value;
        }

        public long CreatedAt => createdAt;
        public bool HasFailed => hasFailed;
        public List<InventoryItem> Items => items;
        public List<CellEvent> CellEvents => cellEvents;

        public int CompareTo(Record other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return createdAt.CompareTo(other.createdAt);
        }

        public void SetCreatedAt(long ticks)
        {
            createdAt = ticks;
        }

        public void SetItems(in List<InventoryItem> itemsIn)
        {
            items = itemsIn;
        }

        public void SetCellEvents(in List<CellEvent> cellEventsIn)
        {
            cellEvents = cellEventsIn;
        }

        public void SetResult(bool hasFailedIn)
        {
            hasFailed = hasFailedIn;
        }
    }
}