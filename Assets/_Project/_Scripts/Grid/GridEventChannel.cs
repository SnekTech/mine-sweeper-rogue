using System;
using SnekTech.C;
using SnekTech.GridCell;
using UnityEngine;

namespace SnekTech.Grid
{
    [CreateAssetMenu(fileName = nameof(GridEventChannel), menuName = MenuName.EventChannels + "/" + nameof(GridEventChannel))]
    public class GridEventChannel : ScriptableObject
    {
        public event Action<IGrid> OnGridInitComplete;
        public event Action<IGrid, ILogicCell> OnBombReveal;
        public event Action<IGrid, ILogicCell> OnCellReveal;
        public event Action<IGrid> OnCellFlagOperateComplete;
        public event Action<IGrid> OnGridClear;
        public event Action<ILogicCell> OnCellRecursiveRevealComplete;

        public void InvokeOnGridInitComplete(IGrid grid) => OnGridInitComplete?.Invoke(grid);

        public void InvokeOnBombReveal(IGrid grid, ILogicCell cell) => OnBombReveal?.Invoke(grid, cell);

        public void InvokeOnCellReveal(IGrid grid, ILogicCell cell) => OnCellReveal?.Invoke(grid, cell);

        public void InvokeOnGridCleared(IGrid grid) => OnGridClear?.Invoke(grid);

        public void InvokeOnCellFlagOperated(IGrid grid) => OnCellFlagOperateComplete?.Invoke(grid);

        public void InvokeOnRecursiveRevealComplete(ILogicCell cell) => OnCellRecursiveRevealComplete?.Invoke(cell);
    }
}
