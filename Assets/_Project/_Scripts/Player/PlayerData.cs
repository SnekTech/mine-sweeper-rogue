using System;
using SnekTech.Grid;
using UnityEngine;

namespace SnekTech.Player
{
    [CreateAssetMenu(fileName = nameof(PlayerData), menuName = nameof(PlayerData))]
    public class PlayerData : ScriptableObject
    {
        [SerializeField]
        private GridEventManager gridEventManager;

        public event Action DataChanged;

        public HealthArmour HealthArmour { get; } = new HealthArmour(10, 10);

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

        private void OnBombRevealed(IGrid grid)
        {
            HealthArmour.TakeDamage(3);
            DataChanged?.Invoke();
        }

        private void OnGridInitCompleted(IGrid grid)
        {
            DataChanged?.Invoke();
        }
    }
}
