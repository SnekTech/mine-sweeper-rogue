using SnekTech.Player;

namespace SnekTech.InventorySystem.Items
{
    public abstract class PlayerDataAccumulator : IPlayerDataAccumulator
    {
        private PlayerDataAccumulator _decoratedAccumulator;

        protected PlayerDataAccumulator(PlayerDataAccumulator decoratedAccumulator)
        {
            _decoratedAccumulator = decoratedAccumulator;
        }


        public void Accumulate(PlayerData playerData)
        {
            _decoratedAccumulator?.Accumulate(playerData);
            DoAccumulate(playerData);
        }

        protected abstract void DoAccumulate(PlayerData playerData);
    }
}