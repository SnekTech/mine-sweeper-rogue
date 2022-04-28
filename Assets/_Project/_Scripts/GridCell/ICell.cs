namespace SnekTech.GridCell
{
    public interface ICell
    {
        CellState CoveredState { get; }
        CellState FlaggedState { get; }
        CellState RevealedState { get; }

        void Reset();
        void SwitchState(CellState state);
        void LiftFlag();
        void RevealCover();
        void OnLeftClick();
        void OnRightClick();
        
        IFlag Flag { get; }
    }
}