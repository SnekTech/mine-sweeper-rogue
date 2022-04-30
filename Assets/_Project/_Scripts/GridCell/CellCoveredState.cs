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
        }

        public override void OnLeaveState()
        {
        }

        public override async void OnLeftClick()
        {
            ICover cover = CellBrain.Cover;
            
            bool isOpenCoverCompleted = await cover.RevealAsync();
            if (isOpenCoverCompleted)
            {
                CellBrain.SwitchState(CellBrain.RevealedState);
            }
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
