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

        public event Action HealthRanOut;

        #endregion
      
        
        [Header("DI")]
        [SerializeField]
        private GridEventManager gridEventManager;

        [SerializeField]
        private Inventory inventory;


        public Inventory Inventory => inventory;

        public int SweepScope => _calculatedPlayerData.sweepScope;
        public int DamagePerBomb => _calculatedPlayerData.damagePerBomb;

        public int Health => _healthArmour.Health;
        public int Armour => _healthArmour.Armour;


        private PlayerData _basicPlayerData;
        private PlayerData _calculatedPlayerData;
        private readonly HealthArmour _healthArmour = HealthArmour.Default;

        private readonly List<IPlayerDataAccumulator> _playerDataAccumulators = new List<IPlayerDataAccumulator>();

        private readonly List<IPlayerStateDisplay> _playerStateDisplays =
            new List<IPlayerStateDisplay>();

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
        
        private async UniTaskVoid OnBombRevealed(IGrid grid, ICell cell)
        {
            await PerformAllDamageEffectAsync(cell);
            
            TakeDamage(DamagePerBomb);
        }
        
        private void TakeDamage(int damage)
        {
            _healthArmour.TakeDamage(damage);
            UpdateAllDisplays();
        }

        public void LoadData(GameData gameData)
        {
            _basicPlayerData = gameData.playerData;
            _healthArmour.ResetWith(gameData.healthArmour);
            
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

        public void AddDataChangeDisplay(IPlayerStateDisplay display)
        {
            display.UpdateDisplay();
            _playerStateDisplays.Add(display);
        }

        private UniTask PerformAllDamageEffectAsync(ICell cell)
        {
            return UniTask.WhenAll(_playerStateDisplays
                .Select(display => display
                    .PerformDamageEffectAsync(cell.WorldPosition, DamagePerBomb)));
        }

        private void UpdateAllDisplays()
        {
            foreach (IPlayerStateDisplay display in _playerStateDisplays)
            {
                display.UpdateDisplay();
            }
        }
    }
}
