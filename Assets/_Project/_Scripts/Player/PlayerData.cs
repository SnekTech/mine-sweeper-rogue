using System;
using SnekTech.Grid;
using SnekTech.GridCell;
using SnekTech.InventorySystem;
using SnekTech.Constants;
using SnekTech.Core;
using UnityEngine;

namespace SnekTech.Player
{
    [CreateAssetMenu(fileName = nameof(PlayerData), menuName = nameof(PlayerData))]
    public class PlayerData : ScriptableObject
    {
        [SerializeField]
        private GridEventManager gridEventManager;

        [SerializeField]
        private Inventory inventory;

        private int _sweepScope = 1;

        private const int DamagePerBomb = GameData.DamagePerBomb;

        public event Action DataChanged;
        public event Action<IGrid, ICell, int> TakenDamage;

        public HealthArmour HealthArmour { get; private set; } = HealthArmour.Default;

        public Inventory Inventory => inventory;

        public int SweepScope
        {
            get => _sweepScope;
            set
            {
                const int min = GameData.SweepScopeMin, max = GameData.SweepScopeMax;
                int originalValue = _sweepScope;
                _sweepScope = Mathf.Clamp(value, min, max);
                
                if (value < min)
                {
                    throw new ReachLimitException<int>(min - originalValue);
                }
                if (value > max)
                {
                    throw new ReachLimitException<int>(max - originalValue);
                }
            }
        }

        private void OnEnable()
        {
            gridEventManager.BombRevealed += OnBombRevealed;
            gridEventManager.GridInitCompleted += OnGridInitCompleted;
        }

        private void OnDisable()
        {
            gridEventManager.BombRevealed -= OnBombRevealed;
            gridEventManager.GridInitCompleted -= OnGridInitCompleted;
        }

        private void OnBombRevealed(IGrid grid, ICell cell)
        {
            HealthArmour.TakeDamage(DamagePerBomb);
            TakenDamage?.Invoke(grid, cell, DamagePerBomb);
        }

        private void OnGridInitCompleted(IGrid grid)
        {
            HealthArmour = HealthArmour.Default;
            InvokeDataChanged();
        }

        public void InvokeDataChanged()
        {
            DataChanged?.Invoke();
        }
    }
}
