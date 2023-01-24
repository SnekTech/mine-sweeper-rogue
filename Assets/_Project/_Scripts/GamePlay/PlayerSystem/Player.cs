using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekTech.DataPersistence;
using SnekTech.GamePlay.AbilitySystem;
using SnekTech.GamePlay.InventorySystem;
using SnekTech.Grid;
using SnekTech.GridCell;
using UnityEngine;

namespace SnekTech.GamePlay.PlayerSystem
{
    [CreateAssetMenu(menuName = C.MenuName.Player + "/" + nameof(Player))]
    public class Player : ScriptableObject, IPlayer, IPersistentDataHolder
    {
        #region Event channels

        [SerializeField]
        private PlayerEventChannel playerEventChannel;
        [SerializeField]
        private GridEventManager gridEventChannel;

        #endregion

        [SerializeField]
        private Inventory inventory;

        [SerializeField]
        private PlayerAbilityHolder abilityHolder;

        [SerializeField]
        private PlayerStats stats;

        private readonly List<IPlayerDataHolder> dataHolders = new List<IPlayerDataHolder>();

        #region Getters

        public Inventory Inventory => inventory;
        public int SweepScope => stats.Calculated.sweepScope;
        public int DamagePerBomb => stats.Calculated.damagePerBomb;
        public int ItemChoiceCount => stats.Calculated.itemChoiceCount;
        
        #endregion

        // load game entry point
        public void OnEnable()
        {
            gridEventChannel.OnBombReveal += HandleOnBombReveal;
            
            stats.Life.HealthRanOut += HandleHealthRanOut;
            abilityHolder.Changed += HandleAbilitiesChanged;
            inventory.ItemsChanged += HandleItemsChanged;
            
            dataHolders.Clear();
            dataHolders.Add(inventory);
            dataHolders.Add(stats);
        }

        public void OnDisable()
        {
            gridEventChannel.OnBombReveal -= HandleOnBombReveal;
            
            stats.Life.HealthRanOut -= HandleHealthRanOut;
            abilityHolder.Changed -= HandleAbilitiesChanged;
            inventory.ItemsChanged -= HandleItemsChanged;
        }

        private void HandleAbilitiesChanged(List<PlayerAbility> abilities) =>
            playerEventChannel.InvokeAbilitiesChanged(abilities);

        private void HandleItemsChanged(List<InventoryItem> items) =>
            playerEventChannel.InvokeInventoryItemChanged(items);

        private void HandleHealthRanOut() => playerEventChannel.InvokeHealthRanOut();
        
        private void HandleOnBombReveal(IGrid grid, ILogicCell cell)
        {
            TakeDamage(DamagePerBomb);
        }

        #region delegate life effects
        
        public void TakeDamage(int damage) => stats.Life.TakeDamage(damage).Forget();

        public void TakeDamageOnHealth(int damage) => stats.Life.TakeDamageOnHealth(damage).Forget();

        public void TakeDamageOnArmour(int damage) => stats.Life.TakeDamageOnArmour(damage).Forget();

        public void AddHealth(int healthIncrement) => stats.Life.AddHealth(healthIncrement).Forget();

        public void AddArmour(int armourIncrement) => stats.Life.AddArmour(armourIncrement).Forget();

        public void AddMaxHealth(int amount) => stats.Life.AdjustMaxHealth(amount);

        #endregion

        
        // todo: change to event driven & async
        public void AddHealthArmourDisplay(IHealthArmourDisplay display)
        {
            stats.Life.AddDisplay(display);
        }
        
        #region delegate ability methods

        public void AddClickAbility(PlayerAbility playerAbility)
        {
            abilityHolder.AddClickAbility(playerAbility);
        }

        public void RemoveClickAbility(PlayerAbility playerAbility)
        {
            abilityHolder.RemoveClickAbility(playerAbility);
        }

        public void UseClickAbilities()
        {
            abilityHolder.UseClickAbilities();
        }

        public void ClearAllAbilities()
        {
            abilityHolder.ClearAll();
        }
        
        #endregion

        public void LoadData(GameData gameData)
        {
            foreach (var dataHolder in dataHolders)
            {
                dataHolder.LoadData(gameData.playerData);
            }
        }

        public void SaveData(GameData gameData)
        {
            foreach (var dataHolder in dataHolders)
            {
                dataHolder.SaveData(gameData.playerData);
            }
        }
    }
}