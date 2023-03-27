using System;
using System.Collections.Generic;
using SnekTech.Core.GameEvent;
using SnekTech.Core.History;
using SnekTech.GamePlay;
using SnekTech.GamePlay.InventorySystem;
using SnekTech.GamePlay.PlayerSystem;
using SnekTech.Roguelike;

namespace SnekTech.DataPersistence
{
    public class GameData
    {
        public SeedData seedData;
        public int currentLevelIndex;

        public PlayerData playerData;
        
        public List<CellEventInstance> cellEvents;

        public HistoryData historyData;

        public GameData()
        {
            seedData = new SeedData();
            currentLevelIndex = 0;

            playerData = new PlayerData();
            
            cellEvents = new List<CellEventInstance>();
            
            historyData = new HistoryData();
        }
    }
}
