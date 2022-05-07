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

        public override Task<bool> OnLeftClick()
        {
            return Task.FromResult(false);
        }

        public override Task<bool> OnRightLick()
        {
            return Task.FromResult(false);
        }
    }
}
