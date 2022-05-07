using System;
using System.Collections.Generic;
using SnekTech.GridCell;

namespace SnekTech
{
    public class BasicGridBrain : IGridBrain
    {
        private readonly IGrid _grid;

        public BasicGridBrain(IGrid grid)
        {
            _grid = grid;
        }
        
        public static readonly GridIndex[] NeighborOffsets =
        {
            new GridIndex(-1, -1),
            new GridIndex(0, -1),
            new GridIndex(1, -1),
            new GridIndex(-1, 0),
            new GridIndex(1, 0),
            new GridIndex(-1, 1),
            new GridIndex(0, 1),
            new GridIndex(1, 1),
        };
        
        public ICell GetCellAt(GridIndex gridIndex)
        {
            return _grid.Cells[gridIndex.RowIndex * _grid.Size.columnCount + gridIndex.ColumnIndex];
        }

        public bool IsIndexWithinGrid(GridIndex gridIndex)
        {
            int rowIndex = gridIndex.RowIndex;
            int columnIndex = gridIndex.ColumnIndex;
            return rowIndex >= 0 && rowIndex < _grid.Size.columnCount && 
                   columnIndex >= 0 && columnIndex < _grid.Size.rowCount;
        }

        public int GetNeighborBombCount(ICell cell)
        {
            int neighborBombCount = 0;
            ForEachNeighbor(cell, neighborCell =>
            {
                if (neighborCell.HasBomb)
                {
                    neighborBombCount++;
                }
            });

            return neighborBombCount;
        }

        public void ForEachNeighbor(ICell cell, Action<ICell> processNeighbor)
        {
            foreach (GridIndex offset in NeighborOffsets)
            {
                GridIndex gridIndex = _grid.CellIndexDict[cell] + offset;
                if (IsIndexWithinGrid(gridIndex))
                {
                    processNeighbor(GetCellAt(gridIndex));
                }
            }
        }
    }
}
