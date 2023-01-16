using Cysharp.Threading.Tasks;
using SnekTech.Core.FiniteStateMachine;
using SnekTech.GridCell.Cover;
using SnekTech.GridCell.Flag;

namespace SnekTech.GridCell.FSM
{
    public abstract class CellState : IState
    {
        protected readonly CellFSM cellFSM;

        protected ILogicFlag Flag => cellFSM.Cell.Flag;
        protected ILogicCover Cover => cellFSM.Cell.Cover;

        protected CellState(CellFSM cellFSM)
        {
            this.cellFSM = cellFSM;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract UniTask<bool> OnPrimary();
        public abstract UniTask<bool> OnSecondary();
    }
}
