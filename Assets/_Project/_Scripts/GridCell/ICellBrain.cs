using System.Threading.Tasks;

namespace SnekTech.GridCell
{
    public interface ICellBrain
    {
        CellState CoveredState { get; }
        CellState FlaggedState { get; }
        CellState RevealedState { get; }

        void SwitchState(CellState state);
        void Reset();
        Task<bool> OnLeftClick();
        Task<bool> OnRightClick();
        IFlag Flag { get; }
        ICover Cover { get; }
    }
}