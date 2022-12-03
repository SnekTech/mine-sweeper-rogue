using SnekTech.Player;

namespace SnekTech.InventorySystem.Items
{
    public abstract class PlayerPropertyModifier : IPlayerDataAccumulator
    {
        private PlayerPropertyModifier _decoratedModifier;

        protected PlayerPropertyModifier(PlayerPropertyModifier decoratedModifier)
        {
            _decoratedModifier = decoratedModifier;
        }


        public void Accumulate(PlayerData playerData)
        {
            _decoratedModifier?.Accumulate(playerData);
            DoAccumulate(playerData);
        }

        protected abstract void DoAccumulate(PlayerData playerData);
    }
}