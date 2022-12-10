namespace SnekTech.Player.ClickEffect
{
    public abstract class ClickEffect : IClickEffect
    {
        private readonly ClickEffect _decoratedEffect;

        protected ClickEffect(ClickEffect decoratedEffect)
        {
            _decoratedEffect = decoratedEffect;
        }
        
        public void Take(PlayerState playerState)
        {
            _decoratedEffect?.Take(playerState);
            DoTake(playerState);
        }

        protected abstract void DoTake(PlayerState playerState);
    }
}
