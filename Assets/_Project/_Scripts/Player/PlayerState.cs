using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekTech.Grid;
using SnekTech.GridCell;
using SnekTech.InventorySystem;
using SnekTech.Constants;
using SnekTech.Core.GameEvent;
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

        #endregion
      
        
        [Header("DI")]
        [SerializeField]
        private GridEventManager gridEventManager;

        [SerializeField]
        private Inventory inventory;

        [SerializeField]
        private GameEventHolder gameEventHolder;


        public Inventory Inventory => inventory;
        public GameEventHolder GameEventHolder => gameEventHolder;

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
        private readonly List<IClickEffect> _clickEffects = new List<IClickEffect>();

        private void OnEnable()
        {
            gridEventManager.BombRevealed += OnBombRevealed;
            _healthArmour.HealthRanOut += OnHealthRanOut;
        }


        private void OnDisable()
        {
            gridEventManager.BombRevealed -= OnBombRevealed;
            _healthArmour.HealthRanOut -= OnHealthRanOut;
        }

        private void OnHealthRanOut()
        {
            HealthRanOut?.Invoke();
        }
        
        private void OnBombRevealed(IGrid grid, ICell cell)
        {
            TakeDamage(DamagePerBomb);
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

            inventory.Load(_basicPlayerData.items);
            CalculatePlayerData();
            
            gameEventHolder.Load(gameData.playerData);
        }

        public void SaveData(GameData gameData)
        {
            _basicPlayerData.items = inventory.Items;
            _basicPlayerData.cellEvents = gameEventHolder.CellEvents;
            
            gameData.playerData = _basicPlayerData;
            gameData.healthArmour = _healthArmour;
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

        public void AddClickEffect(IClickEffect clickEffect)
        {
            _clickEffects.Add(clickEffect);
        }

        public void TriggerAllClickEffects()
        {
            _clickEffects.RemoveAll(clickEffect => !clickEffect.IsActive);

            foreach (IClickEffect clickEffect in _clickEffects)
            {
                clickEffect.Take(this);
            }
        }

        private void ClearAllEffects()
        {
            _playerDataAccumulators.Clear();
            _clickEffects.Clear();
        }
    }
}
