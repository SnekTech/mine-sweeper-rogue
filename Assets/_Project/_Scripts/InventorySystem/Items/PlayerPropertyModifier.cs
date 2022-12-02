using SnekTech.Player;

namespace SnekTech.InventorySystem.Items
{
    public abstract class PlayerPropertyModifier
    {
        private PlayerPropertyModifier _decoratedModifier;

        protected PlayerPropertyModifier(PlayerPropertyModifier decoratedModifier)
        {
            _decoratedModifier = decoratedModifier;
        }


        public void Apply(PlayerState playerState)
        {
            _decoratedModifier?.Apply(playerState);
            DoApply(playerState);
        }

        public void Resume(PlayerState playerState)
        {
            _decoratedModifier?.Resume(playerState);
            DoResume(playerState);
        }

        protected abstract void DoApply(PlayerState playerState);
        protected abstract void DoResume(PlayerState playerState);
    }
}