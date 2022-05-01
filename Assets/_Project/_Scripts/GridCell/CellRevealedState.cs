using System.Threading.Tasks;

namespace SnekTech.GridCell
{
    public class CellRevealedState : CellState
    {
        public CellRevealedState(ICellBrain cellBrain) : base(cellBrain)
        {
        }

        public override void OnEnterState()
        {
            CellBrain.Cover.IsActive = false;
        }

        public override void OnLeaveState()
        {
            
        }

        public override async Task<bool> OnLeftClick()
        {
            ICover cover = CellBrain.Cover;
            cover.IsActive = true;

            bool isPutCoverCompleted = await cover.PutCoverAsync();
            if (isPutCoverCompleted)
            {
                CellBrain.SwitchState(CellBrain.CoveredState);
            }
            
            return isPutCoverCompleted;
        }

        public override Task<bool> OnRightLick()
        {
            return Task.FromResult(true);
        }
    }
}
