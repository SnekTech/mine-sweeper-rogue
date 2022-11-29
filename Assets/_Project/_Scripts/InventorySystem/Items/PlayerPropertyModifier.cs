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


        public void Apply(PlayerData playerData)
        {
            _decoratedModifier?.Apply(playerData);
            DoApply(playerData);
        }

        public void Resume(PlayerData playerData)
        {
            _decoratedModifier?.Resume(playerData);
            DoResume(playerData);
        }

        protected abstract void DoApply(PlayerData playerData);
        protected abstract void DoResume(PlayerData playerData);
    }
}