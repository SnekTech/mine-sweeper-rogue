using System;
using UnityEngine;

namespace SnekTech.Grid
{
    [CreateAssetMenu(fileName = nameof(GridEventManager), menuName = Utils.MyEventManagerMenuName + nameof(GridEventManager))]
    public class GridEventManager : ScriptableObject
    {
        public event Action<IGrid> GridInitCompleted;
        public event Action BombRevealed;
        public event Action<int> EmptyCellRevealed;
        public event Action GridCleared;

        public void InvokeGridInitCompleted(IGrid grid)
        {
            GridInitCompleted?.Invoke(grid);
        }
        
        public void InvokeBombRevealed()
        {
            BombRevealed?.Invoke();
        }

        public void InvokeEmptyCellRevealed(int remainingEmptyCellCount)
        {
            EmptyCellRevealed?.Invoke(remainingEmptyCellCount);
        }

        public void InvokeGridCleared()
        {
            GridCleared?.Invoke();
        }
    }
}
