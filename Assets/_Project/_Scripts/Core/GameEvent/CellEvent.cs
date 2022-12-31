using System;
using SnekTech.Grid;
using UnityEngine;

namespace SnekTech.Core.GameEvent
{
    [Serializable]
    public class CellEvent
    {
        [SerializeField]
        private CellEventData cellEventData;

        [SerializeField]
        private bool isActive = true;

        [SerializeField]
        private GridIndex gridIndex;

        [SerializeField]
        private int levelIndex;
        
        public CellEventData CellEventData => cellEventData;
        public bool IsActive => isActive;
        public int LevelIndex => levelIndex;
        public GridIndex GridIndex => gridIndex;
        
        public CellEvent(CellEventData cellEventData, GridIndex gridIndex, int levelIndex)
        {
            this.gridIndex = gridIndex;
            this.levelIndex = levelIndex;
            this.cellEventData = cellEventData;

            cellEventData.Completed += OnCellEventDataCompleted;
        }

        private void OnCellEventDataCompleted()
        {
            isActive = false;
            cellEventData.Completed -= OnCellEventDataCompleted;
        }
    }
}