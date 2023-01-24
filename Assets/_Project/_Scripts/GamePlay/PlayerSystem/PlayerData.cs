using SnekTech.GamePlay.InventorySystem;

namespace SnekTech.GamePlay.PlayerSystem
{
    public class PlayerData
    {
        public InventoryData inventoryData;
        public PlayerStatsData playerStatsData;

        public PlayerData()
        {
            inventoryData = new InventoryData();
            playerStatsData = new PlayerStatsData();
        }}
}
