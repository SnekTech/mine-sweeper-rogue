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
        
        protected override void DoApply(PlayerData playerData)
        {
            try
            {
                playerData.SweepScope += _changeAmount;
            }
            catch (ReachLimitException<int> reachLimitException)
            {
                _changeAmount = reachLimitException.ActualChangeAmount;
            }
        }

        protected override void DoResume(PlayerData playerData)
        {
            playerData.SweepScope -= _changeAmount;
        }
    }
}
