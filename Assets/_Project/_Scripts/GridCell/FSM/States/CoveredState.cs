using Cysharp.Threading.Tasks;

namespace SnekTech.GridCell.FSM
{
    public class CoveredState : CellState
    {
        public CoveredState(CellFSM cellFSM) : base(cellFSM)
        {
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override async UniTask<bool> OnPrimary()
        {
            cellFSM.ChangeState(cellFSM.RevealedState);
            bool isRevealSuccessful = await Cover.RevealAsync();

            return isRevealSuccessful;
        }

        public override async UniTask<bool> OnSecondary()
        {
            cellFSM.ChangeState(cellFSM.FlaggedState);

            bool isLifFlagSuccessful = await Flag.LiftAsync();

            return isLifFlagSuccessful;
        }
    }
}