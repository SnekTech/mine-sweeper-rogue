using System;
using SnekTech.Grid;
using SnekTech.GridCell;
using SnekTech.InventorySystem;
using UnityEngine;

namespace SnekTech.Player
{
    [CreateAssetMenu(fileName = nameof(PlayerData), menuName = nameof(PlayerData))]
    public class PlayerData : ScriptableObject
    {
        [SerializeField]
        private GridEventManager gridEventManager;

        private const int DamagePerBomb = 3;

        public event Action DataChanged;
        public event Action<IGrid, ICell, int> TakenDamage;

        public HealthArmour HealthArmour { get; private set; } = HealthArmour.Default;

        [SerializeField]
        private Inventory inventory = new Inventory();

        public Inventory Inventory => inventory;

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
