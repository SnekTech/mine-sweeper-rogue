namespace SnekTech.Player.ClickEffect
{
    // player data effect only performs finite times
    public interface IClickEffect
    {
        bool IsActive { get; }
        void Take(PlayerState playerState);
    }
}
