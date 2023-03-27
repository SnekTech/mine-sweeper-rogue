using System;
using System.Collections.Generic;
using SnekTech.Core.GameEvent;
using SnekTech.GamePlay.InventorySystem;
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
        private List<CellEventInstance> cellEvents;


        public string Seed
        {
            get => seed;
            set => seed = value;
        }

        public long CreatedAt
        {
            get => createdAt;
            set => createdAt = value;
        }

        public bool HasFailed
        {
            get => hasFailed;
            set => hasFailed = value;
        }

        public List<InventoryItem> Items
        {
            get => items;
            set => items = value;
        }

        public List<CellEventInstance> CellEvents
        {
            get => cellEvents;
            set => cellEvents = value;
        }

        public int CompareTo(Record other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return createdAt.CompareTo(other.createdAt);
        }
    }
}