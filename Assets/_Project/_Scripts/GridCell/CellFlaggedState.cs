namespace SnekTech.GridCell
{
    public class CellFlaggedState : CellState
    {
        public CellFlaggedState(CellBehaviour cellBehaviour) : base(cellBehaviour)
        {
        }

        public override void OnEnterState()
        {
            CellBehaviour.LiftFlag();
        }

        public override void OnLeftClick()
        {
        }

        public override void OnRightLick()
        {
            CellBehaviour.PutDownFlag();
            CellBehaviour.SwitchState(CellBehaviour.CoveredState);
        }
    }
}
