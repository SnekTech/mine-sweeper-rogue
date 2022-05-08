using System;
using System.Collections.Generic;
using SnekTech.GridCell;

namespace SnekTech.Grid
{
    public interface IGrid : ICanClickAsync
    {
        Dictionary<ICell, GridIndex> CellIndexDict { get; }
        List<ICell> Cells { get; }
        GridSize Size { get; }

        event Action BombRevealed;
        event Action Cleared;
        
        int CellCount { get; }
        int BombCount { get; }
        int RevealedCellCount { get; }
        
        void InitCells();
        void InitCells(GridSize gridSize);
        void DisposeCells();
        void ResetCells();
    }
}