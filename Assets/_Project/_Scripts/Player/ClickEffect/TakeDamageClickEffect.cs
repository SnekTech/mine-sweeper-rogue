namespace SnekTech.Player.ClickEffect
{
    public class TakeDamageClickEffect : IClickEffect
    {
        private readonly int _damagePerClick;
        private readonly IClickEffect _decoratedClickEffect;
        private bool _isActive = true;

        public bool IsActive => _isActive;

        public TakeDamageClickEffect(int damagePerClick, IClickEffect decoratedClickEffect = null)
        {
            _damagePerClick = damagePerClick;
            _decoratedClickEffect = decoratedClickEffect;
        }
        
        public void Take(PlayerState playerState)
        {
            _decoratedClickEffect?.Take(playerState);
            _isActive = false;
            
            playerState.TakeDamage(_damagePerClick);
        }
    }
}
