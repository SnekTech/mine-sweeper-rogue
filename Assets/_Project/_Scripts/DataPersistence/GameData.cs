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
    [Serializable]
    public class GameData
    {
        public SeedData seedData;
        public int currentLevelIndex;

        public Player player;
        
        public List<CellEvent> cellEvents;

        public HistoryData historyData;

        public GameData()
        {
            seedData = new SeedData();
            currentLevelIndex = 0;

            player = new Player();
            
            cellEvents = new List<CellEvent>();
            
            historyData = new HistoryData();
        }
    }
}
