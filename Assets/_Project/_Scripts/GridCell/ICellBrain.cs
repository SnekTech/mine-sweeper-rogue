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
        void RevealCover();
        void OnLeftClick();
        void OnRightClick();
        IFlag Flag { get; }
    }
}