using System.Threading.Tasks;

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

        public override void OnLeaveState()
        {
            
        }

        public override void OnLeftClick()
        {
        }

        public override async void OnRightLick()
        {
            bool isPutDownCompleted = await CellBrain.PutDownFlagAsync();
            if (isPutDownCompleted)
            {
                CellBrain.SwitchState(CellBrain.CoveredState);
            }
        }
    }
}
