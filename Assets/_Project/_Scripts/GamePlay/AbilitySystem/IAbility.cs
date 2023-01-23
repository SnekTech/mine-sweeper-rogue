using SnekTech.UI;

namespace SnekTech.GamePlay.AbilitySystem
{
    /// <summary>
    /// An ability will be used multiple times during the life cycle.
    /// </summary>
    /// <typeparam name="T">target type</typeparam>
    public interface IAbility<in T> : IHoverableIconHolder
    {
        bool IsActive { get; }
        void Init();
        void Use(T target);
    }
}