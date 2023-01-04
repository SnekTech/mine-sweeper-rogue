using SnekTech.Player.OneTimeEffect;
using SnekTech.UI;

namespace SnekTech.Player.ClickEffect
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
        
        public void Take(PlayerState playerState)
        {
            _decoratedClickEffect?.Take(playerState);
            
            playerState.TakeDamage(DamagePerClick);
        }
    }
}