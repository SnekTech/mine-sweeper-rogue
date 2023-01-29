using Cysharp.Threading.Tasks;
using SnekTech.Core.FiniteStateMachine;

namespace SnekTech.GridCell.FSM
{
    public class CellFSM : FSM<CellState>
    {
        public readonly ILogicCell Cell;
        
        public readonly CoveredState CoveredState;
        public readonly FlaggedState FlaggedState;
        public readonly RevealedState RevealedState;

        public bool IsCovered => Current == CoveredState;
        public bool IsFlagged => Current == FlaggedState;
        public bool IsRevealed => Current == RevealedState;
        
        public CellFSM(ILogicCell cell)
        {
            Cell = cell;
            CoveredState = new CoveredState(this);
            FlaggedState = new FlaggedState(this);
            RevealedState = new RevealedState(this);
            
            Init(CoveredState);
        }
    }
}