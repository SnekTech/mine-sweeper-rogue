using Cysharp.Threading.Tasks;
using SnekTech.Core.FiniteStateMachine;

namespace SnekTech.GridCell.FSM
{
    public class CellFSM : FSM<CellState>
    {
        public readonly ICell Cell;
        
        public readonly CoveredState CoveredState;
        public readonly FlaggedState FlaggedState;
        public readonly RevealedState RevealedState;

        public bool IsCovered => current == CoveredState;
        public bool IsFlagged => current == FlaggedState;
        public bool IsRevealed => current == RevealedState;
        
        public CellFSM(ICell cell)
        {
            Cell = cell;
            CoveredState = new CoveredState(this);
            FlaggedState = new FlaggedState(this);
            RevealedState = new RevealedState(this);
            
            Init(CoveredState);
        }

        public UniTask<bool> OnPrimary() => current.OnPrimary();
        public UniTask<bool> OnSecondary() => current.OnSecondary();
    }
}