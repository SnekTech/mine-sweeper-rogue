using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekTech.GamePlay.AbilitySystem;
using SnekTech.GamePlay.InventorySystem;
using SnekTech.Grid;
using SnekTech.GridCell;

namespace SnekTech.GamePlay.PlayerSystem
{
    public class Player : IPlayer
    {
        #region Event channels

        private PlayerEventChannel _playerEventChannel;
        private GridEventManager _gridEventChannel;

        #endregion

        private readonly Inventory _inventory;

        private readonly PlayerStats _stats;

        private readonly PlayerAbilityHolder _playerAbilityHolder;

        public Player()
        {
            _inventory = new Inventory(this);
            _stats = new PlayerStats();
            _playerAbilityHolder = new PlayerAbilityHolder(this);
        }
        
        
        #region Getters

        public Inventory Inventory => _inventory;
        public int SweepScope => _stats.Calculated.sweepScope;
        public int DamagePerBomb => _stats.Calculated.damagePerBomb;
        public int ItemChoiceCount => _stats.Calculated.itemChoiceCount;
        
        #endregion

        // load game entry point
        public void OnEnable(PlayerEventChannel playerEventChannel, GridEventManager gridEventManager)
        {
            _playerEventChannel = playerEventChannel;
            _gridEventChannel = gridEventManager;
            
            _gridEventChannel.OnBombReveal += HandleOnBombReveal;
            _stats.Life.HealthRanOut += HandleHealthRanOut;
            _playerAbilityHolder.Changed += HandlePlayerAbilitiesChanged;
            _inventory.ItemsChanged += HandleItemsChanged;
        }

        public void OnDisable()
        {
            _gridEventChannel.OnBombReveal -= HandleOnBombReveal;
            _stats.Life.HealthRanOut -= HandleHealthRanOut;
            _playerAbilityHolder.Changed -= HandlePlayerAbilitiesChanged;
        }

        private void HandlePlayerAbilitiesChanged(List<PlayerAbility> abilities) =>
            _playerEventChannel.InvokeAbilitiesChanged(abilities);

        private void HandleItemsChanged(List<InventoryItem> items) =>
            _playerEventChannel.InvokeInventoryItemChanged(items);

        private void HandleHealthRanOut() => _playerEventChannel.InvokeHealthRanOut();
        
        private void HandleOnBombReveal(IGrid grid, ILogicCell cell)
        {
            TakeDamage(DamagePerBomb);
        }

        #region delegate life effects
        
        public void TakeDamage(int damage) => _stats.Life.TakeDamage(damage).Forget();

        public void TakeDamageOnHealth(int damage) => _stats.Life.TakeDamageOnHealth(damage).Forget();

        public void TakeDamageOnArmour(int damage) => _stats.Life.TakeDamageOnArmour(damage).Forget();

        public void AddHealth(int healthIncrement) => _stats.Life.AddHealth(healthIncrement).Forget();

        public void AddArmour(int armourIncrement) => _stats.Life.AddArmour(armourIncrement).Forget();

        public void AddMaxHealth(int amount) => _stats.Life.AdjustMaxHealth(amount);

        #endregion

        
        // todo: change to event driven & async
        public void AddHealthArmourDisplay(IHealthArmourDisplay display)
        {
            _stats.Life.AddDisplay(display);
        }
        
        #region delegate ability methods

        public void AddClickAbility(PlayerAbility playerAbility)
        {
            _playerAbilityHolder.AddClickAbility(playerAbility);
        }

        public void RemoveClickAbility(PlayerAbility playerAbility)
        {
            _playerAbilityHolder.RemoveClickAbility(playerAbility);
        }

        public void UseClickAbilities()
        {
            _playerAbilityHolder.UseClickAbilities();
        }

        public void ClearAllAbilities()
        {
            _playerAbilityHolder.ClearAll();
        }
        
        #endregion
    }
}