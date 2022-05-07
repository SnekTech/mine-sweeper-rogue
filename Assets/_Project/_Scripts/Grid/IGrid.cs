using System.Collections.Generic;
using SnekTech.GridCell;

namespace SnekTech.Grid
{
    public interface IGrid
    {
        Dictionary<ICell, GridIndex> CellIndexDict { get; }
        List<ICell> Cells { get; }
        GridSize Size { get; }
        
        void InitCells();
        void DisposeCells();
        void ResetCells();
    }
}