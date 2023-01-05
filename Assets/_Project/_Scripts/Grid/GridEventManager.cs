﻿using System;
using SnekTech.Constants;
using SnekTech.GridCell;
using UnityEngine;

namespace SnekTech.Grid
{
    [CreateAssetMenu(fileName = nameof(GridEventManager), menuName = MenuName.EventManager + MenuName.Slash + nameof(GridEventManager))]
    public class GridEventManager : ScriptableObject
    {
        public event Action<IGrid> GridInitCompleted;
        public event Action<IGrid, ICell> BombRevealed;
        public event Action<IGrid> CellRevealed;
        public event Action<IGrid> CellFlagOperated;
        public event Action<IGrid> GridCleared;

        public void InvokeGridInitCompleted(IGrid grid)
        {
            GridInitCompleted?.Invoke(grid);
        }
        
        public void InvokeBombRevealed(IGrid grid, ICell cell)
        {
            BombRevealed?.Invoke(grid, cell);
        }

        public void InvokeCellRevealed(IGrid grid)
        {
            CellRevealed?.Invoke(grid);
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
