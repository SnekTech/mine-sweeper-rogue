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

        public override async Task<bool> OnLeftClick()
        {
            ICover cover = CellBrain.Cover;
            
            bool isRevealCompleted = await cover.RevealAsync();
            if (isRevealCompleted)
            {
                CellBrain.SwitchState(CellBrain.RevealedState);
            }

            return isRevealCompleted;
        }

        public override async Task<bool> OnRightLick()
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

            return isLiftFlagCompleted;
        }
    }
}
