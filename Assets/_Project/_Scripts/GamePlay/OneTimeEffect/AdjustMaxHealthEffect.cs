using SnekTech.GamePlay.PlayerSystem;

namespace SnekTech.GamePlay.OneTimeEffect
{
    public class AdjustMaxHealthEffect : IOneTimeEffect
    {
        private readonly int _amount;
        private readonly IOneTimeEffect _decoratedEffect;

        public AdjustMaxHealthEffect(int amount, IOneTimeEffect decoratedEffect = null)
        {
            _amount = amount;
            _decoratedEffect = decoratedEffect;
        }
        
        public void Take(Player player)
        {
            _decoratedEffect?.Take(player);
            
            player.AddMaxHealth(_amount);
        }
    }
}
