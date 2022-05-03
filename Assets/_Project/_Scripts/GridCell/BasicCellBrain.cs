using System;
using System.Threading.Tasks;

namespace SnekTech.GridCell
{
    public class BasicCellBrain : ICellBrain
    {
        public CellState CoveredState { get; private set; }
        public CellState FlaggedState { get; private set; }
        public CellState RevealedState { get; private set; }

        private readonly ICell _cell;
        private CellState _currentState;

        public bool HasBomb { get; private set; }
        public IFlag Flag => _cell.Flag;
        public ICover Cover => _cell.Cover;

        public bool IsFlagged => _currentState == FlaggedState;
        public bool IsCovered => _currentState == CoveredState;

        public BasicCellBrain(ICell cell)
        {
            _cell = cell;
            
            InitStates();
            Reset();
        }

        private void InitStates()
        {
            CoveredState ??= new CellCoveredState(this);
            FlaggedState ??= new CellFlaggedState(this);
            RevealedState ??= new CellRevealedState(this);
        }

        public void SetBomb()
        {
            HasBomb = true;
        }

        public void RemoveBomb()
        {
            HasBomb = false;
        }

        public void Reset()
        {
            SwitchState(CoveredState);
        }

        public void SwitchState(CellState state)
        {
            _currentState?.OnLeaveState();
            _currentState = state;
            _currentState.OnEnterState();
        }

        public Task<bool> OnLeftClick()
        {
            return _currentState.OnLeftClick();
        }

        public Task<bool> OnRightClick()
        {
            return _currentState.OnRightLick();
        }
    }
}
