using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using SnekTech.Grid;
using SnekTech.GridCell;
using SnekTech.InventorySystem;
using SnekTech.Constants;
using SnekTech.DataPersistence;
using SnekTech.Player.ClickEffect;
using SnekTech.Player.PlayerDataAccumulator;
using UnityEngine;

namespace SnekTech.Player
{
    [CreateAssetMenu(fileName = nameof(PlayerState), menuName = nameof(PlayerState))]
    public class PlayerState : ScriptableObject, IPersistentDataHolder
    {
        #region Events

        public event Action HealthRanOut;
        public event Action ClickEffectsChanged;

        #endregion
      
        
        [Header("DI")]
        [SerializeField]
        private GridEventManager gridEventManager;

        [SerializeField]
        private Inventory inventory;


        public Inventory Inventory => inventory;

        public int SweepScope => _calculatedPlayerData.sweepScope;
        public int DamagePerBomb => _calculatedPlayerData.damagePerBomb;
        public int ItemChoiceCount => _calculatedPlayerData.itemChoiceCount;

        public int Health => _healthArmour.Health;
        public int MaxHealth => _healthArmour.MaxHealth;
        public int Armour => _healthArmour.Armour;


        private PlayerData _basicPlayerData;
        private PlayerData _calculatedPlayerData;
        private readonly HealthArmour _healthArmour = HealthArmour.Default;

        private readonly List<IPlayerDataAccumulator> _playerDataAccumulators = new List<IPlayerDataAccumulator>();

        private List<IClickEffect> ClickEffects => new List<IClickEffect>
        {
            LacerationEffect,
        };
        
        public List<IClickEffect> ActiveClickEffects => ClickEffects.Where(effect => effect.IsActive).ToList();

        public readonly LacerationEffect LacerationEffect = new LacerationEffect();

        private void OnEnable()
        {
            gridEventManager.OnBombReveal += HandleOnBombReveal;
            _healthArmour.HealthRanOut += OnHealthRanOut;

            foreach (IClickEffect clickEffect in ClickEffects)
            {
                clickEffect.Changed += OnClickEffectChanged;
            }
        }


        private void OnDisable()
        {
            gridEventManager.OnBombReveal -= HandleOnBombReveal;
            _healthArmour.HealthRanOut -= OnHealthRanOut;
            
            foreach (IClickEffect clickEffect in ClickEffects)
            {
                clickEffect.Changed -= OnClickEffectChanged;
            }
        }

        private void OnHealthRanOut()
        {
            HealthRanOut?.Invoke();
        }
        
        private void HandleOnBombReveal(IGrid grid, ICell cell)
        {
            TakeDamage(DamagePerBomb);
        }

        private void OnClickEffectChanged()
        {
            ClickEffectsChanged?.Invoke();
        }
        
        public void TakeDamage(int damage)
        {
            _healthArmour.TakeDamage(damage).Forget();
        }

        public void TakeDamageOnHealth(int damage)
        {
            _healthArmour.TakeDamageOnHealth(damage).Forget();
        }

        public void TakeDamageOnArmour(int damage)
        {
            _healthArmour.TakeDamageOnArmour(damage).Forget();
        }

        public void AddHealth(int healthIncrement)
        {
            _healthArmour.AddHealth(healthIncrement).Forget();
        }

        public void AddArmour(int armourIncrement)
        {
            _healthArmour.AddArmour(armourIncrement).Forget();
        }

        public void AdjustMaxHealth(int amount)
        {
            _healthArmour.AdjustMaxHealth(amount);
        }

        // entry point
        public void LoadData(GameData gameData)
        {
            _basicPlayerData = gameData.playerData;
            _healthArmour.ResetWith(gameData.healthArmour);

            ClearAllEffects();

            _playerDataAccumulators.Clear();
            inventory.Load(gameData.items);
            CalculatePlayerData();
        }

        public void SaveData(GameData gameData)
        {
            gameData.playerData = _basicPlayerData;
            gameData.healthArmour = _healthArmour;
            gameData.items = inventory.Items;
        }

        private void CalculatePlayerData()
        {
            var dataCopy = new PlayerData(_basicPlayerData);
            foreach (IPlayerDataAccumulator accumulator in _playerDataAccumulators)
            {
                accumulator.Accumulate(dataCopy);
            }

            dataCopy.sweepScope = Mathf.Clamp(dataCopy.sweepScope, GameConstants.SweepScopeMin, GameConstants.SweepScopeMax);

            _calculatedPlayerData = dataCopy;
        }

        public void AddDataAccumulator(IPlayerDataAccumulator accumulator)
        {
            _playerDataAccumulators.Add(accumulator);
            CalculatePlayerData();
        }

        public void RemoveDataAccumulator(IPlayerDataAccumulator accumulator)
        {
            _playerDataAccumulators.Remove(accumulator);
            CalculatePlayerData();
        }

        public void AddHealthArmourDisplay(IHealthArmourDisplay display)
        {
            _healthArmour.AddDisplay(display);
        }

        public void TriggerAllClickEffects()
        {
            foreach (IClickEffect effect in ActiveClickEffects)
            {
                effect.Take(this);
            }
        }

        public void ClearAllEffects()
        {
            foreach (IClickEffect clickEffect in ClickEffects)
            {
                clickEffect.IsActive = false;
            }
        }
    }
}