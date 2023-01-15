using System;
using SnekTech.C;
using SnekTech.GridCell;
using UnityEngine;

namespace SnekTech.Grid
{
    [CreateAssetMenu(fileName = nameof(GridEventManager), menuName = MenuName.EventManager + MenuName.Slash + nameof(GridEventManager))]
    public class GridEventManager : ScriptableObject
    {
        public event Action<IGrid> OnGridInitComplete;
        public event Action<IGrid, ICell> OnBombReveal;
        public event Action<IGrid, ICell> OnCellReveal;
        public event Action<IGrid, ICell> OnCellFlagOperateComplete;
        public event Action<IGrid> OnGridClear;
        public event Action<ICell> OnCellRecursiveRevealComplete;

        public void InvokeOnGridInitComplete(IGrid grid) => OnGridInitComplete?.Invoke(grid);

        public void InvokeOnBombReveal(IGrid grid, ICell cell) => OnBombReveal?.Invoke(grid, cell);

        public void InvokeOnCellReveal(IGrid grid, ICell cell) => OnCellReveal?.Invoke(grid, cell);

        public void InvokeOnGridCleared(IGrid grid) => OnGridClear?.Invoke(grid);

        public void InvokeOnCellFlagOperated(IGrid grid, ICell cell) => OnCellFlagOperateComplete?.Invoke(grid, cell);

        public void InvokeOnRecursiveRevealComplete(ICell cell) => OnCellRecursiveRevealComplete?.Invoke(cell);
    }
}
