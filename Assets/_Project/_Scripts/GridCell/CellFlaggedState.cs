namespace SnekTech.GridCell
{
    public class CellFlaggedState : CellState
    {
        public CellFlaggedState(ICell cell) : base(cell)
        {
        }

        public override void OnEnterState()
        {
            Cell.LiftFlag();
        }

        public override void OnLeftClick()
        {
        }

        public override void OnRightLick()
        {
            Cell.PutDownFlag();
            Cell.SwitchState(Cell.CoveredState);
        }
    }
}
