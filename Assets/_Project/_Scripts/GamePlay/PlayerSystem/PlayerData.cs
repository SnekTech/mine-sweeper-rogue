using System.Collections.Generic;
using SnekTech.GamePlay.AbilitySystem;
using SnekTech.GamePlay.InventorySystem;

namespace SnekTech.GamePlay.PlayerSystem
{
    public class PlayerData
    {
        public InventoryData inventoryData;
        public PlayerStatsData playerStatsData;
        public AbilityData abilityData;

        public PlayerData()
        {
            inventoryData = new InventoryData();
            playerStatsData = new PlayerStatsData();
            abilityData = new AbilityData();
        }
    }

    public class AbilityData
    {
        public Dictionary<string, int> repeatTimesByAbilityName;

        public AbilityData()
        {
            repeatTimesByAbilityName = new Dictionary<string, int>();
            foreach (var ability in PlayerAbilityRepo.Instance.Assets)
            {
                repeatTimesByAbilityName.Add(ability.name, 0);
            }
        }
    }
}