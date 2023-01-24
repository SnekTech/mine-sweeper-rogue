using SnekTech.GamePlay.PlayerSystem;

namespace SnekTech.GamePlay.PlayerDataAccumulator
{
    public class SweepScopeAccumulator : PlayerStatsDataAccumulator
    {
        private int _changeAmount;

        public SweepScopeAccumulator(int changeAmount, PlayerStatsDataAccumulator decoratedAccumulator = null)
            : base(decoratedAccumulator)
        {
            _changeAmount = changeAmount;
        }

        protected override void DoAccumulate(PlayerStatsData playerStatsData)
        {
            playerStatsData.sweepScope += _changeAmount;
        }
    }
}
