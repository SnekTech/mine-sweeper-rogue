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
        }

        public override void OnLeaveState()
        {
            CellBrain.Flag.IsActive = false;
        }

        public override Task<bool> OnLeftClick()
        {
            return Task.FromResult(true);
        }

        public override async Task<bool> OnRightLick()
        {
            bool isPutDownCompleted = await CellBrain.Flag.PutDownAsync();
            if (isPutDownCompleted)
            {
                CellBrain.SwitchState(CellBrain.CoveredState);
            }

            return isPutDownCompleted;
        }
    }
}
