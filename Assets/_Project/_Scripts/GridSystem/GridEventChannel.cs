﻿using System;
using SnekTech.C;
using SnekTech.MineSweeperRogue.GridSystem;
using SnekTech.MineSweeperRogue.GridSystem.CellSystem;
using UnityEngine;

namespace SnekTech.GridSystem
{
    [CreateAssetMenu(fileName = nameof(GridEventChannel), menuName = MenuName.EventChannels + "/" + nameof(GridEventChannel))]
    public class GridEventChannel : ScriptableObject
    {
        public event Action<IGrid> OnGridInitComplete;
        public event Action<IGrid, ICell> OnBombReveal;
        public event Action<IGrid, ICell> OnCellReveal;
        public event Action<IGrid> OnCellFlagOperateComplete;
        public event Action<IGrid> OnGridClear;
        public event Action<ICell> OnCellRecursiveRevealComplete;

        public void InvokeOnGridInitComplete(IGrid grid) => OnGridInitComplete?.Invoke(grid);

        public void InvokeOnBombReveal(IGrid grid, ICell cell) => OnBombReveal?.Invoke(grid, cell);

        public void InvokeOnCellReveal(IGrid grid, ICell cell) => OnCellReveal?.Invoke(grid, cell);

        public void InvokeOnGridCleared(IGrid grid) => OnGridClear?.Invoke(grid);

        public void InvokeOnCellFlagOperated(IGrid grid) => OnCellFlagOperateComplete?.Invoke(grid);

        public void InvokeOnRecursiveRevealComplete(ICell cell) => OnCellRecursiveRevealComplete?.Invoke(cell);
    }
}
