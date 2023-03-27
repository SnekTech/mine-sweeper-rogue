using System;
using Newtonsoft.Json;
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
        
        [JsonIgnore]
        public CellEventData CellEventData => cellEventData;

        public string Name
        {
            get => cellEventData.name;
            set => cellEventData = CellEventAssetRepo.Instance.Get(value);
        }
        
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