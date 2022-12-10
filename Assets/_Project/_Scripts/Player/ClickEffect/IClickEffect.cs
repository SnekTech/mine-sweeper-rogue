namespace SnekTech.Player.ClickEffect
{
    // player data effect only performs finite times
    public interface IClickEffect
    {
        void Take(PlayerState playerState);
    }
}
