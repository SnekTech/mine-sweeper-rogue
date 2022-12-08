using System;
using SnekTech.Player;

namespace SnekTech.DataPersistence
{
    [Serializable]
    public class GameData
    {
        public PlayerData playerData;
        public HealthArmour healthArmour;

        public GameData()
        {
            playerData = new PlayerData();
            healthArmour = HealthArmour.Default;
        }
    }
}
