using Cysharp.Threading.Tasks;

namespace SnekTech.GridCell
{
    public class CellRevealedState : CellState
    {
        public CellRevealedState(ICellBrain cellBrain) : base(cellBrain)
        {
        }

        public override void OnEnterState()
        {
            Cover.IsActive = false;
        }

        public override void OnLeaveState()
        {
            
        }

        public override UniTask<bool> OnLeftClick()
        {
            return UniTask.FromResult(false);
        }

        public override UniTask<bool> OnRightLick()
        {
            return UniTask.FromResult(false);
        }
    }
}
