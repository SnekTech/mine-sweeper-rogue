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

        protected override void DoApply(PlayerState playerState)
        {
            playerState.HealthArmour.AddHealth(_changeAmount);
        }

        protected override void DoResume(PlayerState playerState)
        {
            playerState.HealthArmour.AddHealth(-_changeAmount);
        }
    }
}
