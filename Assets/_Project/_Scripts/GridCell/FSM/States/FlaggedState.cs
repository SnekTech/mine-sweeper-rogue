using Cysharp.Threading.Tasks;

namespace SnekTech.GridCell.FSM
{
    public class FlaggedState : CellState
    {
        public FlaggedState(CellFSM cellFSM) : base(cellFSM)
        {
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override UniTask<bool> OnReveal()
        {
            return UniTask.FromResult(false);
        }

        public override async UniTask<bool> OnSwitchFlag()
        {
            bool isPutDownSuccessful = await Flag.PutDownAsync();
            if (isPutDownSuccessful)
            {
                cellFSM.ChangeState(cellFSM.CoveredState);
            }

            return isPutDownSuccessful;
        }
    }
}
