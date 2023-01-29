using SnekTech.GamePlay.AbilitySystem;
using SnekTech.GamePlay.InventorySystem;
using SnekTech.GamePlay.WeaponSystem;

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

        void AddClickAbility(PlayerAbility playerAbility);
        void RemoveClickAbility(PlayerAbility playerAbility);
        void UseClickAbilities();
        void ClearAllAbilities();

        #endregion
        
        Inventory Inventory { get; }
        IWeapon Weapon { get; }
    }
}