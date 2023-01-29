using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekTech.GridCell;

namespace SnekTech.Grid
{
    public interface IGrid : ICanClickAsync
    {
        List<ICell> Cells { get; }
        ICell GetCellAt(GridIndex gridIndex);
        GridData GridData { get; }
        
        int CellCount { get; }
        int BombCount { get; }
        int RevealedCellCount { get; }
        int FlaggedCellCount { get; }

        void InitCells(GridData newGridData);
        UniTask RevealCellAsync(GridIndex gridIndex);
    }
}