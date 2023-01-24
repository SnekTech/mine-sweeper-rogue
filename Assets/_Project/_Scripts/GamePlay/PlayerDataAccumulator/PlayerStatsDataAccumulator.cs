using SnekTech.GamePlay.PlayerSystem;

namespace SnekTech.GamePlay.PlayerDataAccumulator
{
    public abstract class PlayerStatsDataAccumulator : IPlayerStatsDataAccumulator
    {
        private PlayerStatsDataAccumulator _decoratedAccumulator;

        protected PlayerStatsDataAccumulator(PlayerStatsDataAccumulator decoratedAccumulator)
        {
            _decoratedAccumulator = decoratedAccumulator;
        }


        public void Accumulate(PlayerStatsData playerStatsData)
        {
            _decoratedAccumulator?.Accumulate(playerStatsData);
            DoAccumulate(playerStatsData);
        }

        protected abstract void DoAccumulate(PlayerStatsData playerStatsData);
    }
}