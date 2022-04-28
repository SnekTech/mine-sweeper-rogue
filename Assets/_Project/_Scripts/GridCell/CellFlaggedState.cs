namespace SnekTech.GridCell
{
    public class CellFlaggedState : CellState
    {
        public CellFlaggedState(ICellBrain cellBrain) : base(cellBrain)
        {
        }

        public override void OnEnterState()
        {
            CellBrain.LiftFlag();
        }

        public override void OnLeftClick()
        {
        }

        public override void OnRightLick()
        {
            CellBrain.PutDownFlag();
            CellBrain.SwitchState(CellBrain.CoveredState);
        }
    }
}
