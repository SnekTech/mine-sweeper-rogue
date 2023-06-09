﻿using Cysharp.Threading.Tasks;
using SnekTech.GamePlay.AbilitySystem;
using SnekTech.GamePlay.InventorySystem;
using SnekTech.GamePlay.WeaponSystem;
using SnekTech.MineSweeperRogue.GridSystem.CellSystem;

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

        void AddAbility(PlayerAbility ability, int repeatTimes);
        void RemoveAbility(PlayerAbility playerAbility);
        UniTask UseAbilities();
        void ClearAllAbilities();

        #endregion
        
        Inventory Inventory { get; }

        UniTask UsePrimary(ICell cell);
        UniTask UseSecondary(ICell cell);
    }
}