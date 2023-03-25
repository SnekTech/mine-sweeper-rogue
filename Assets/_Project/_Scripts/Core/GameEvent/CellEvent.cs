using System;
using SnekTech.MineSweeperRogue.GridSystem;
using UnityEngine;

namespace SnekTech.Core.GameEvent
{
    [Serializable]
    public class CellEvent
    {
        [SerializeField]
        private CellEventData cellEventData;

        [SerializeField]
        private GridIndex gridIndex;

        [SerializeField]
        private int levelIndex;
        
        public CellEventData CellEventData => cellEventData;
        public int LevelIndex => levelIndex;
        public GridIndex GridIndex => gridIndex;
        
        public CellEvent(CellEventData cellEventData, GridIndex gridIndex, int levelIndex)
        {
            this.gridIndex = gridIndex;
            this.levelIndex = levelIndex;
            this.cellEventData = cellEventData;
        }
    }
}