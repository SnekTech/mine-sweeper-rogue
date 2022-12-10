using System;
using SnekTech.Core.History;
using SnekTech.Player;

namespace SnekTech.DataPersistence
{
    [Serializable]
    public class GameData
    {
        public PlayerData playerData;
        public HealthArmour healthArmour;
        public HistoryData historyData;

        public GameData()
        {
            playerData = new PlayerData();
            healthArmour = HealthArmour.Default;
            historyData = new HistoryData();
        }
    }
}
