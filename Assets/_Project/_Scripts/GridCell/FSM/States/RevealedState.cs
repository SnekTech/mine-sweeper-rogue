using Cysharp.Threading.Tasks;

namespace SnekTech.GridCell.FSM
{
    public class RevealedState : CellState
    {
        public RevealedState(CellFSM cellFSM) : base(cellFSM)
        {
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override UniTask<bool> OnPrimary()
        {
            return UniTask.FromResult(false);
        }

        public override UniTask<bool> OnSecondary()
        {
            return UniTask.FromResult(false);
        }
    }
}
