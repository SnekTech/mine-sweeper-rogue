using System;
using System.Collections.Generic;
using SnekTech.Grid;
using SnekTech.GridCell;
using SnekTech.InventorySystem;
using SnekTech.Constants;
using SnekTech.DataPersistence;
using SnekTech.InventorySystem.Items;
using UnityEngine;

namespace SnekTech.Player
{
    [CreateAssetMenu(fileName = nameof(PlayerState), menuName = nameof(PlayerState))]
    public class PlayerState : ScriptableObject, IPersistentDataHolder
    {
        #region Events

        public event Action DataChanged;
        public event Action<IGrid, ICell, int> TakenDamage;

        #endregion
      
        
        [SerializeField]
        private GridEventManager gridEventManager;

        [SerializeField]
        private Inventory inventory;

        private PlayerData _basicPlayerData;
        private PlayerData _calculatedPlayerData;

        private readonly List<IPlayerDataAccumulator> _playerDataAccumulators = new List<IPlayerDataAccumulator>();

        public Inventory Inventory => inventory;

        public int SweepScope => _calculatedPlayerData.sweepScope;
        public int DamagePerBomb => _calculatedPlayerData.damagePerBomb;
        public HealthArmour HealthArmour => _calculatedPlayerData.healthArmour;


        private void OnEnable()
        {
            gridEventManager.BombRevealed += OnBombRevealed;
            DataChanged += OnPlayerDataChanged;
        }


        private void OnDisable()
        {
            gridEventManager.BombRevealed -= OnBombRevealed;
            DataChanged -= OnPlayerDataChanged;
        }

        private void OnPlayerDataChanged()
        {
            CalculatePlayerData();
        }
        
        private void OnBombRevealed(IGrid grid, ICell cell)
        {
            _basicPlayerData.healthArmour.TakeDamage(DamagePerBomb);
            TakenDamage?.Invoke(grid, cell, DamagePerBomb);
        }

        public void LoadData(GameData gameData)
        {
            _basicPlayerData = gameData.playerData;
            inventory.Load(_basicPlayerData.items);
            CalculatePlayerData();
        }

        public void SaveData(GameData gameData)
        {
            _basicPlayerData.items = inventory.Items;
            gameData.playerData = _basicPlayerData;
        }

        public void InvokeDataChanged()
        {
            DataChanged?.Invoke();
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
            InvokeDataChanged();
        }

        public void RemoveDataAccumulator(IPlayerDataAccumulator accumulator)
        {
            _playerDataAccumulators.Remove(accumulator);
            InvokeDataChanged();
        }
    }
}
