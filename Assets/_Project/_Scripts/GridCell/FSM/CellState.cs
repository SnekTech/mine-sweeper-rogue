using Cysharp.Threading.Tasks;
using SnekTech.Core.FiniteStateMachine;
using SnekTech.GridCell.Cover;
using SnekTech.GridCell.Flag;

namespace SnekTech.GridCell.FSM
{
    public interface ICellState
    {
        UniTask<bool> OnPrimary();
        UniTask<bool> OnSecondary();
    }
    
    public abstract class CellState : IState, ICellState
    {
        protected readonly CellFSM cellFSM;

        protected IFlag Flag => cellFSM.Cell.Flag;
        protected ICover Cover => cellFSM.Cell.Cover;

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
