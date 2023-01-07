using System;
using System.Collections.Generic;
using SnekTech.Core.GameEvent;
using SnekTech.Core.History;
using SnekTech.InventorySystem;
using SnekTech.Player;
using SnekTech.Roguelike;

namespace SnekTech.DataPersistence
{
    [Serializable]
    public class GameData
    {
        public SeedData seedData;
        
        public PlayerData playerData;
        public HealthArmour healthArmour;
        public List<InventoryItem> items;
        public List<CellEvent> cellEvents;

        public HistoryData historyData;

        public GameData()
        {
            seedData = new SeedData();
            
            playerData = new PlayerData();
            healthArmour = HealthArmour.Default;
            items = new List<InventoryItem>();
            cellEvents = new List<CellEvent>();
            
            historyData = new HistoryData();
        }
    }
}
