using SnekTech.Player;

namespace SnekTech.InventorySystem.Items
{
    public class HealthModifier : PlayerPropertyModifier
    {
        private readonly int _changeAmount;

        public HealthModifier(int changeAmount, PlayerPropertyModifier decoratedModifier = null)
            : base(decoratedModifier)
        {
            _changeAmount = changeAmount;
        }

        protected override void DoAccumulate(PlayerData playerData)
        {
            playerData.healthArmour.AddHealth(_changeAmount);
        }
    }
}
