using System;
using SnekTech.Core;
using SnekTech.Player;

namespace SnekTech.InventorySystem.Items
{
    public class SweepScopeModifier : PlayerPropertyModifier
    {
        private int _changeAmount;

        public SweepScopeModifier(int changeAmount, PlayerPropertyModifier decoratedModifier = null)
            : base(decoratedModifier)
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
