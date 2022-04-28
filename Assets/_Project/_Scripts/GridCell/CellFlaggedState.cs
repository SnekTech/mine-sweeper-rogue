namespace SnekTech.GridCell
{
    public class CellFlaggedState : CellState
    {
        public CellFlaggedState(Cell cell) : base(cell)
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
