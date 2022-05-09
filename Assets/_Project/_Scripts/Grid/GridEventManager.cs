using System;
using UnityEngine;

namespace SnekTech.Grid
{
    [CreateAssetMenu(fileName = nameof(GridEventManager), menuName = Utils.MyEventManagerMenuName + nameof(GridEventManager))]
    public class GridEventManager : ScriptableObject
    {
        public event Action BombRevealed;
        public event Action<int> EmptyCellRevealed;
        public event Action GridCleared;

        public void OnBombRevealed()
        {
            BombRevealed?.Invoke();
        }

        public void OnEmptyCellRevealed(int remainingEmptyCellCount)
        {
            EmptyCellRevealed?.Invoke(remainingEmptyCellCount);
        }

        public void OnGridCleared()
        {
            GridCleared?.Invoke();
        }
    }
}
