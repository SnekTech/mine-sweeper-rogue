using SnekTech.GamePlay.PlayerSystem;
using SnekTech.UI;

namespace SnekTech.GamePlay.AbilitySystem
{
    public interface IPlayerAbility : IHoverableIconHolder
    {
        void Use(IPlayer player);
    }
}