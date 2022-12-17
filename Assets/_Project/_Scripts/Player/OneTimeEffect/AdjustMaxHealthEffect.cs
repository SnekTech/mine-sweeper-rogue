namespace SnekTech.Player.OneTimeEffect
{
    public class AdjustMaxHealthEffect : IOneTimeEffect
    {
        private readonly int _amount;
        private IOneTimeEffect _decoratedEffect;

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
