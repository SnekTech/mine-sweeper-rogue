using System;
using SnekTech.GridCell;

namespace SnekTech.Grid
{
    public interface IGridBrain
    {
        ICell GetCellAt(GridIndex gridIndex);
        bool IsIndexWithinGrid(GridIndex gridIndex);
        int GetNeighborBombCount(ICell cell);
        void ForEachNeighbor(ICell cell, Action<ICell> processNeighbor);
    }
}