using System;
using System.Collections.Generic;
using SnekTech.GridCell;

namespace SnekTech.Grid
{
    public interface IGridBrain
    {
        bool IsIndexWithinGrid(GridIndex gridIndex);
        void ForEachNeighbor(ICell cell, Action<ICell> processNeighbor);
        int GetNeighborBombCount(ICell cell);
        int GetNeighborFlagCount(ICell cell);
        List<ICell> GetAffectedCellsWithinScope(ICell cellHovering, int sweepScope);
    }
}