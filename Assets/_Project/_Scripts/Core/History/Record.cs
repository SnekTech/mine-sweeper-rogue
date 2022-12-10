using System;
using System.Collections.Generic;
using SnekTech.Core.GameEvent;
using SnekTech.Player;
using UnityEngine;

namespace SnekTech.Core.History
{
    [Serializable]
    public class Record : IComparable<Record>
    {
        [SerializeField]
        private List<CellEvent> cellEvents = new List<CellEvent>();

        [SerializeField]
        private long createdAt;

        private PlayerState _playerState;

        public List<CellEvent> CellEvents => cellEvents;

        public Record(PlayerState playerState)
        {
            _playerState = playerState;
        }

        public int CompareTo(Record other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return createdAt.CompareTo(other.createdAt);
        }

        public void SetCreatedAt(long ticks)
        {
            createdAt = ticks;
        }

        public void AddCellEvent(CellEvent cellEvent)
        {
            cellEvent.Emit(_playerState);
            cellEvents.Add(cellEvent);
        }
    }
}
