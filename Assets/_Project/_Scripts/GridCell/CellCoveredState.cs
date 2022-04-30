using System.Threading.Tasks;

namespace SnekTech.GridCell
{
    public class CellCoveredState : CellState
    {
        public CellCoveredState(ICellBrain cellBrain) : base(cellBrain)
        {
        }

        public override void OnEnterState()
        {
            CellBrain.Flag.IsActive = false;
            // TODO: cover the cover
        }

        public override void OnLeaveState()
        {
        }

        public override void OnLeftClick()
        {
            CellBrain.SwitchState(CellBrain.RevealedState);
        }

        public override async void OnRightLick()
        {
            if (!CellBrain.Flag.IsActive)
            {
                CellBrain.Flag.IsActive = true;
            }

            bool isLiftFlagCompleted = await CellBrain.Flag.LiftAsync();
            if (isLiftFlagCompleted)
            {
                CellBrain.SwitchState(CellBrain.FlaggedState);
            }
        }
    }
}
