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
            CellBrain.HideFlag();
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
            bool isLiftFlagCompleted = await CellBrain.LiftFlagAsync();
            if (isLiftFlagCompleted)
            {
                CellBrain.SwitchState(CellBrain.FlaggedState);
            }
        }
    }
}
