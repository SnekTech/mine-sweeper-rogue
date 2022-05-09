using System;
using UnityEngine;

namespace SnekTech.Grid
{
    [CreateAssetMenu(fileName = nameof(GridEventManager), menuName = Utils.MyEventManagerMenuName + nameof(GridEventManager))]
    public class GridEventManager : ScriptableObject
    {
        public event Action<IGrid> GridInitCompleted;
        public event Action<IGrid> BombRevealed;
        public event Action<IGrid> EmptyCellRevealed;
        public event Action<IGrid> CellFlagOperated;
        public event Action<IGrid> GridCleared;

        public void InvokeGridInitCompleted(IGrid grid)
        {
            GridInitCompleted?.Invoke(grid);
        }
        
        public void InvokeBombRevealed(IGrid grid)
        {
            BombRevealed?.Invoke(grid);
        }

        public void InvokeEmptyCellRevealed(IGrid grid)
        {
            EmptyCellRevealed?.Invoke(grid);
        }

        public void InvokeGridCleared(IGrid grid)
        {
            GridCleared?.Invoke(grid);
        }

        public void InvokeCellFlagOperated(IGrid grid)
        {
            CellFlagOperated?.Invoke(grid);
        }
    }
}
