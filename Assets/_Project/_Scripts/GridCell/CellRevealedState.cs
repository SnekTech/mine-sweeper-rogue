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

        public override async void OnLeftClick()
        {
            ICover cover = CellBrain.Cover;
            cover.IsActive = true;

            bool isCloseCompleted = await cover.PutCoverAsync();
            if (isCloseCompleted)
            {
                CellBrain.SwitchState(CellBrain.CoveredState);
            }
        }

        public override void OnRightLick()
        {
        }
    }
}
