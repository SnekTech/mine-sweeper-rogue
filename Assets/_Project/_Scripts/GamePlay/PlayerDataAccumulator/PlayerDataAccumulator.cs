using SnekTech.GamePlay.PlayerSystem;

namespace SnekTech.GamePlay.PlayerDataAccumulator
{
    public abstract class PlayerDataAccumulator : IPlayerDataAccumulator
    {
        private PlayerDataAccumulator _decoratedAccumulator;

        protected PlayerDataAccumulator(PlayerDataAccumulator decoratedAccumulator)
        {
            _decoratedAccumulator = decoratedAccumulator;
        }


        public void Accumulate(PlayerStats playerStats)
        {
            _decoratedAccumulator?.Accumulate(playerStats);
            DoAccumulate(playerStats);
        }

        protected abstract void DoAccumulate(PlayerStats playerStats);
    }
}