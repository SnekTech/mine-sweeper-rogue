using Cysharp.Threading.Tasks;

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
        }

        public override UniTask<bool> OnLeftClick()
        {
            return UniTask.FromResult(false);
        }

        public override async UniTask<bool> OnRightLick()
        {
            bool isPutDownCompleted = await Flag.PutDownAsync();
            if (isPutDownCompleted)
            {
                CellBrain.SwitchState(CellBrain.CoveredState);
            }

            return isPutDownCompleted;
        }
    }
}
