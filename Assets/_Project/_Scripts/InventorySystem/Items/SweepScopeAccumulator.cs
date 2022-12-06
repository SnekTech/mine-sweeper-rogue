using System;
using SnekTech.Core;
using SnekTech.Player;

namespace SnekTech.InventorySystem.Items
{
    public class SweepScopeAccumulator : PlayerDataAccumulator
    {
        private int _changeAmount;

        public SweepScopeAccumulator(int changeAmount, PlayerDataAccumulator decoratedAccumulator = null)
            : base(decoratedAccumulator)
        {
            _changeAmount = changeAmount;
        }

        protected override void DoAccumulate(PlayerData playerData)
        {
            try
            {
                playerData.sweepScope += _changeAmount;
            }
            catch (ReachLimitException<int> reachLimitException)
            {
                // swallow for now
                Console.WriteLine(reachLimitException);
            }
        }
    }
}
