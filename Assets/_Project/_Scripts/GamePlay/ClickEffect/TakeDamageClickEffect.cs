using SnekTech.GamePlay.OneTimeEffect;
using SnekTech.GamePlay.PlayerSystem;

namespace SnekTech.GamePlay.ClickEffect
{
    public class TakeDamageClickEffect : IOneTimeEffect
    {
        private readonly IClickEffect _decoratedClickEffect;

        public int DamagePerClick { get; set; }

        public TakeDamageClickEffect(int damagePerClick, IClickEffect decoratedClickEffect = null)
        {
            DamagePerClick = damagePerClick;
            _decoratedClickEffect = decoratedClickEffect;
        }
        
        public void Take(Player player)
        {
            _decoratedClickEffect?.Take(player);
            
            player.TakeDamage(DamagePerClick);
        }
    }
}