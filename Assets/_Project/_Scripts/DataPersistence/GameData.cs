using System;
using SnekTech.Player;

namespace SnekTech.DataPersistence
{
    [Serializable]
    public class GameData
    {
        public PlayerData playerData;

        public GameData()
        {
            playerData = new PlayerData();
        }
    }
}
