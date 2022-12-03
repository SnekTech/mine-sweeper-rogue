using System;
using SnekTech.Constants;

namespace SnekTech.Player
{
    [Serializable]
    public class PlayerData
    {
        public int damagePerBomb;
        public int sweepScope;
        public int itemChoiceCount;
        public HealthArmour healthArmour;

        public PlayerData()
        {
            damagePerBomb = GameData.DamagePerBomb;
            sweepScope = GameData.SweepScopeMin;
            itemChoiceCount = GameData.InitialItemChoiceCount;
            healthArmour = HealthArmour.Default;
        }

        public PlayerData(PlayerData other)
        {
            damagePerBomb = other.damagePerBomb;
            sweepScope = other.sweepScope;
            itemChoiceCount = other.itemChoiceCount;
            healthArmour = new HealthArmour(other.healthArmour.Health, other.healthArmour.Armour);
        }
    }
}
