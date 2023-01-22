using SnekTech.GamePlay.AbilitySystem;

namespace SnekTech.GamePlay.PlayerSystem
{
    public interface IPlayer
    {
        #region life effects
        
        void TakeDamage(int amount);
        void TakeDamageOnHealth(int amount);
        void TakeDamageOnArmour(int amount);
        void AddHealth(int amount);
        void AddArmour(int amount);
        void AddMaxHealth(int amount);
        
        #endregion

        #region ability

        void AddClickAbility(IPlayerAbility playerAbility);
        void RemoveClickAbility(IPlayerAbility playerAbility);
        void UseClickAbilities();
        void ClearAllAbilities();

        #endregion
    }
}