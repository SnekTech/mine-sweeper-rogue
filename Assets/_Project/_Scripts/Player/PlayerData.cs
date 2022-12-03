using System;
using System.Collections.Generic;
using SnekTech.Constants;
using SnekTech.InventorySystem;

namespace SnekTech.Player
{
    [Serializable]
    public class PlayerData
    {
        public int damagePerBomb;
        public int sweepScope;
        public int itemChoiceCount;
        public HealthArmour healthArmour;
        public List<InventoryItem> items;

        public PlayerData()
        {
            damagePerBomb = GameConstants.DamagePerBomb;
            sweepScope = GameConstants.SweepScopeMin;
            itemChoiceCount = GameConstants.InitialItemChoiceCount;
            healthArmour = HealthArmour.Default;
            items = new List<InventoryItem>();
        }

        public PlayerData(PlayerData other)
        {
            damagePerBomb = other.damagePerBomb;
            sweepScope = other.sweepScope;
            itemChoiceCount = other.itemChoiceCount;
            healthArmour = new HealthArmour(other.healthArmour.Health, other.healthArmour.Armour);
            items = new List<InventoryItem>(other.items);
        }
    }
}
