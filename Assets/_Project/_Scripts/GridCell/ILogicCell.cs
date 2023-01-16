using Cysharp.Threading.Tasks;
using SnekTech.Grid;
using SnekTech.GridCell.Cover;
using SnekTech.GridCell.Flag;

namespace SnekTech.GridCell
{
    public interface ILogicCell
    {
        GridIndex GridIndex { get; set; }
        IFlag Flag { get; }
        ICover Cover { get; }

        UniTask<bool> OnPrimary();
        UniTask<bool> OnSecondary();

        bool HasBomb { get; set; }
        bool IsFlagged { get; }
        bool IsCovered { get; }
        bool IsRevealed { get; }
    }
}