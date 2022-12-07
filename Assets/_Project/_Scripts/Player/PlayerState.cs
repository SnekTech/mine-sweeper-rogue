using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekTech.Grid;
using SnekTech.GridCell;
using SnekTech.InventorySystem;
using SnekTech.Constants;
using SnekTech.DataPersistence;
using SnekTech.Player.PlayerDataAccumulator;
using UnityEngine;

namespace SnekTech.Player
{
    [CreateAssetMenu(fileName = nameof(PlayerState), menuName = nameof(PlayerState))]
    public class PlayerState : ScriptableObject, IPersistentDataHolder
    {
        #region Events

        public event Action DataChanged;

        #endregion
      
        
        [Header("DI")]
        [SerializeField]
        private GridEventManager gridEventManager;

        [SerializeField]
        private Inventory inventory;


        public Inventory Inventory => inventory;

        public int SweepScope => _calculatedPlayerData.sweepScope;
        public int DamagePerBomb => _calculatedPlayerData.damagePerBomb;
        public HealthArmour HealthArmour => _calculatedPlayerData.healthArmour;


        private PlayerData _basicPlayerData;
        private PlayerData _calculatedPlayerData;

        private readonly List<IPlayerDataAccumulator> _playerDataAccumulators = new List<IPlayerDataAccumulator>();

        private readonly List<IPlayerDataChangePerformer> _playerDataChangePerformers =
            new List<IPlayerDataChangePerformer>();

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
        
        private async UniTaskVoid OnBombRevealed(IGrid grid, ICell cell)
        {
            await UniTask.WhenAll(_playerDataChangePerformers
                .Select(performer => performer.PerformDamage(cell.WorldPosition, DamagePerBomb)));
            _basicPlayerData.healthArmour.TakeDamage(DamagePerBomb);
            InvokeDataChanged();
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

        private void InvokeDataChanged()
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

        public void AddDataChangePerformer(IPlayerDataChangePerformer performer)
        {
            _playerDataChangePerformers.Add(performer);
        }
    }
}
