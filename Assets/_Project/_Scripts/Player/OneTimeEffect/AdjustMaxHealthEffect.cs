namespace SnekTech.Player.OneTimeEffect
{
    public class AdjustMaxHealthEffect : IOneTimeEffect
    {
        // bug: when loading from disk, the item maybe activated
        // again, but max health is also loaded, conflict
        private readonly int _amount;
        private readonly IOneTimeEffect _decoratedEffect;

        public AdjustMaxHealthEffect(int amount, IOneTimeEffect decoratedEffect = null)
        {
            _amount = amount;
            _decoratedEffect = decoratedEffect;
        }
        
        public void Take(PlayerState playerState)
        {
            _decoratedEffect?.Take(playerState);
            
            playerState.AdjustMaxHealth(_amount);
        }
    }
}
