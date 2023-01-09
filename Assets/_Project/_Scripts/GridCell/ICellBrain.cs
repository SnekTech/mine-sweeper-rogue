using Cysharp.Threading.Tasks;

namespace SnekTech.GridCell
{
    public interface ICellBrain
    {
        CellState CoveredState { get; }
        CellState FlaggedState { get; }
        CellState RevealedState { get; }

        void SwitchState(CellState state);
        UniTask<bool> OnLeftClick();
        UniTask<bool> OnRightClick();
        ICell Cell { get; }
        bool IsFlagged { get; }
        bool IsCovered { get; }
        bool IsRevealed { get; }
    }
}