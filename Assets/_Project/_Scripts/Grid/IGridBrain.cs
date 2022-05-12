using System;
using SnekTech.GridCell;

namespace SnekTech.Grid
{
    public interface IGridBrain
    {
        ICell GetCellAt(GridIndex gridIndex);
        bool IsIndexWithinGrid(GridIndex gridIndex);
        void ForEachNeighbor(ICell cell, Action<ICell> processNeighbor);
        int GetNeighborBombCount(ICell cell);
        int GetNeighborFlagCount(ICell cell);
    }
}