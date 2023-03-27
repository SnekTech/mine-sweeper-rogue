using System;
using Newtonsoft.Json;
using SnekTech.MineSweeperRogue.GridSystem;
using UnityEngine;

namespace SnekTech.GamePlay.CellEventSystem
{
    [Serializable]
    public class CellEventInstance
    {
        [SerializeField]
        private CellEvent cellEvent;

        [SerializeField]
        private GridIndex gridIndex;

        [SerializeField]
        private int levelIndex;
        
        [JsonIgnore]
        public CellEvent CellEvent => cellEvent;

        public string Name
        {
            get => cellEvent.name;
            set => cellEvent = CellEventAssetRepo.Instance.Get(value);
        }
        
        public int LevelIndex => levelIndex;
        public GridIndex GridIndex => gridIndex;
        
        public CellEventInstance(CellEvent cellEvent, GridIndex gridIndex, int levelIndex)
        {
            this.gridIndex = gridIndex;
            this.levelIndex = levelIndex;
            this.cellEvent = cellEvent;
        }
    }
}