namespace SnekTech.Player.ClickEffect
{
    public interface IClickEffect
    {
        bool IsActive { get; }
        void Take(PlayerState playerState);
    }
}