using System;
using System.Collections.Generic;
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
            foreach (var offset in NeighborOffsets)
            {
                var gridIndex = cell.GridIndex + offset;
                if (IsIndexWithinGrid(gridIndex))
                {
                    processNeighbor(_grid.GetCellAt(gridIndex));
                }
            }
        }

        public int GetNeighborFlagCount(ICell cell)
        {
            int count = 0;
            ForEachNeighbor(cell, neighborCell =>
            {
                if (neighborCell.IsFlagged)
                {
                    count++;
                }
            });

            return count;
        }

        public List<ICell> GetAffectedCellsWithinScope(ICell cellHovering, int sweepScope)
        {
            // todo: better handle the scope when near the edge, do it in weapon system
            int cornerOffset = sweepScope / 2;
            var topLeftIndex = new GridIndex(cellHovering.GridIndex);
            topLeftIndex.RowIndex -= cornerOffset;
            topLeftIndex.ColumnIndex -= cornerOffset;

            var affectedCells = new List<ICell>();
            for (int i = 0; i < sweepScope; i++)
            {
                for (int j = 0; j < sweepScope; j++)
                {
                    var cellIndex = new GridIndex(topLeftIndex.RowIndex + i, topLeftIndex.ColumnIndex + j);
                    if (IsIndexWithinGrid(cellIndex))
                    {
                        affectedCells.Add(_grid.GetCellAt(cellIndex));
                    }
                }
            }

            return affectedCells;
        }
    }
}
