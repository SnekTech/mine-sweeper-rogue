using SnekTech.Player;

namespace SnekTech.InventorySystem.Items
{
    public class MaxHealthAccumulator : PlayerDataAccumulator
    {
        private readonly int _changeAmount;

        public MaxHealthAccumulator(int changeAmount, PlayerDataAccumulator decoratedAccumulator = null)
            : base(decoratedAccumulator)
        {
            _changeAmount = changeAmount;
        }

        protected override void DoAccumulate(PlayerData playerData)
        {
            // todo: add max-health related feature
            playerData.healthArmour.AddHealth(_changeAmount);
        }
    }
}
