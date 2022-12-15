using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekTech.Grid;
using SnekTech.GridCell;
using SnekTech.InventorySystem;
using SnekTech.Constants;
using SnekTech.Core.GameEvent;
using SnekTech.Core.History;
using SnekTech.DataPersistence;
using SnekTech.Player.ClickEffect;
using SnekTech.Player.PlayerDataAccumulator;
using SnekTech.Roguelike;
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
        private CellEventPool cellEventPool;

        [SerializeField]
        private Inventory inventory;


        public Inventory Inventory => inventory;

        public int SweepScope => _calculatedPlayerData.sweepScope;
        public int DamagePerBomb => _calculatedPlayerData.damagePerBomb;

        public int Health => _healthArmour.Health;
        public int Armour => _healthArmour.Armour;

        public Record CurrentRecord => _currentRecord;


        private PlayerData _basicPlayerData;
        private PlayerData _calculatedPlayerData;
        private readonly HealthArmour _healthArmour = HealthArmour.Default;
        private Record _currentRecord;

        // todo: deal with magic number
        // todo: set to zero for debug, remove later
        private readonly IRandomSequence<bool> _cellEventGenerator = new RandomBoolSequence(0, 0f);

        private readonly List<IPlayerDataAccumulator> _playerDataAccumulators = new List<IPlayerDataAccumulator>();
        private readonly List<IClickEffect> _clickEffects = new List<IClickEffect>();

        private void OnEnable()
        {
            gridEventManager.BombRevealed += OnBombRevealed;
            gridEventManager.CellRevealOperated += OnCellRevealOperated;
            _healthArmour.HealthRanOut += OnHealthRanOut;
        }


        private void OnDisable()
        {
            gridEventManager.BombRevealed -= OnBombRevealed;
            gridEventManager.CellRevealOperated -= OnCellRevealOperated;
            _healthArmour.HealthRanOut -= OnHealthRanOut;
        }

        private void OnHealthRanOut()
        {
            HealthRanOut?.Invoke();
        }
        
        private void OnBombRevealed(IGrid grid, ICell cell)
        {
            TakeDamage(DamagePerBomb).Forget();
        }

        private void OnCellRevealOperated()
        {
            if (_cellEventGenerator.Next())
            {
                _currentRecord.AddCellEvent(new CellEvent(cellEventPool.GetRandom()));
            }
        }
        
        public async UniTaskVoid TakeDamage(int damage)
        {
            await _healthArmour.TakeDamage(damage);
        }

        // entry point
        public void LoadData(GameData gameData)
        {
            _basicPlayerData = gameData.playerData;
            _healthArmour.ResetWith(gameData.healthArmour);

            _currentRecord = new Record(this);
            _playerDataAccumulators.Clear();
            _clickEffects.Clear();
            
            inventory.Load(_basicPlayerData.items);
            CalculatePlayerData();
        }

        public void SaveData(GameData gameData)
        {
            _basicPlayerData.items = inventory.Items;
            
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

        public void RemoveClickEffect(IClickEffect clickEffect)
        {
            _clickEffects.Remove(clickEffect);
        }

        public void TriggerAllClickEffects()
        {
            foreach (IClickEffect clickEffect in _clickEffects)
            {
                clickEffect.Take(this);
            }
        }
    }
}
