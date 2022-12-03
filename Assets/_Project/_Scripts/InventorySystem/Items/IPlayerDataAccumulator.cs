using SnekTech.Player;

namespace SnekTech.InventorySystem.Items
{
    public interface IPlayerDataAccumulator
    {
        void Accumulate(PlayerData playerData);
    }
}