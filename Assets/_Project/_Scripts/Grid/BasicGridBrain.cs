using System;
using SnekTech.GridCell;

namespace SnekTech.Grid
{
    public class BasicGridBrain : IGridBrain
    {
        private readonly IGrid _grid;

        private GridSize GridSize => _grid.GridData.GridSize;

        public BasicGridBrain(IGrid grid)
        {
            _grid = grid;
        }

        private static readonly GridIndex[] NeighborOffsets =
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
            return _grid.Cells[gridIndex.RowIndex * GridSize.columnCount + gridIndex.ColumnIndex];
        }

        public bool IsIndexWithinGrid(GridIndex gridIndex)
        {
            int rowIndex = gridIndex.RowIndex;
            int columnIndex = gridIndex.ColumnIndex;
            return rowIndex >= 0 && rowIndex < GridSize.columnCount && 
                   columnIndex >= 0 && columnIndex < GridSize.rowCount;
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
