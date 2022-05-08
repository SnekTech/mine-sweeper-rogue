using System;
using System.Collections.Generic;
using SnekTech.GridCell;

namespace SnekTech.Grid
{
    public interface IGrid : ICanClickAsync
    {
        Dictionary<ICell, GridIndex> CellIndexDict { get; }
        List<ICell> Cells { get; }
        GridData GridData { get; }

        event Action BombRevealed;
        event Action EmptyRevealed;
        event Action Cleared;
        
        int CellCount { get; }
        int BombCount { get; }
        int RevealedCellCount { get; }
        
        void InitCells();
        void InitCells(GridData gridData);
        void DisposeCells();
        void ResetCells();
    }
}