using System.Collections.Generic;
using SnekTech.GridCell;

namespace SnekTech.Grid
{
    public interface IGrid : ICanClickAsync
    {
        List<ICell> Cells { get; }
        GridData GridData { get; }
        
        int CellCount { get; }
        int BombCount { get; }
        int RevealedCellCount { get; }
        int FlaggedCellCount { get; }

        void InitCells(GridData newGridData);
    }
}