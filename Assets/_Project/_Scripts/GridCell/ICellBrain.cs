namespace SnekTech.GridCell
{
    public interface ICellBrain
    {
        public CellState CoveredState { get; }
        public CellState FlaggedState { get; }
        public CellState RevealedState { get; }

        void SwitchState(CellState state);
        void Reset();
        void LiftFlag();
        void PutDownFlag();
        void HideFlag();
        void RevealCover();
        void OnLeftClick();
        void OnRightClick();
    }
}