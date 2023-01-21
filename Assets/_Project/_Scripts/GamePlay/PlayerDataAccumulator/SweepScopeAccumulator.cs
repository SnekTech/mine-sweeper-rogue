using SnekTech.GamePlay.PlayerSystem;

namespace SnekTech.GamePlay.PlayerDataAccumulator
{
    public class SweepScopeAccumulator : PlayerDataAccumulator
    {
        private int _changeAmount;

        public SweepScopeAccumulator(int changeAmount, PlayerDataAccumulator decoratedAccumulator = null)
            : base(decoratedAccumulator)
        {
            _changeAmount = changeAmount;
        }

        protected override void DoAccumulate(PlayerStats playerStats)
        {
            playerStats.sweepScope += _changeAmount;
        }
    }
}
