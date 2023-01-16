using System;
using SnekTech.C;
using SnekTech.GridCell;
using UnityEngine;

namespace SnekTech.Grid
{
    [CreateAssetMenu(fileName = nameof(GridEventManager), menuName = MenuName.EventManagers + "/" + nameof(GridEventManager))]
    public class GridEventManager : ScriptableObject
    {
        public event Action<IGrid> OnGridInitComplete;
        public event Action<IGrid, ILogicCell> OnBombReveal;
        public event Action<IGrid, ILogicCell> OnCellReveal;
        public event Action<IGrid, ILogicCell> OnCellFlagOperateComplete;
        public event Action<IGrid> OnGridClear; // bug: sometimes can't win a level
        public event Action<ILogicCell> OnCellRecursiveRevealComplete;

        public void InvokeOnGridInitComplete(IGrid grid) => OnGridInitComplete?.Invoke(grid);

        public void InvokeOnBombReveal(IGrid grid, ILogicCell cell) => OnBombReveal?.Invoke(grid, cell);

        public void InvokeOnCellReveal(IGrid grid, ILogicCell cell) => OnCellReveal?.Invoke(grid, cell);

        public void InvokeOnGridCleared(IGrid grid) => OnGridClear?.Invoke(grid);

        public void InvokeOnCellFlagOperated(IGrid grid, ILogicCell cell) => OnCellFlagOperateComplete?.Invoke(grid, cell);

        public void InvokeOnRecursiveRevealComplete(ILogicCell cell) => OnCellRecursiveRevealComplete?.Invoke(cell);
    }
}
