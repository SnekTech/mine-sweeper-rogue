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
        public Cover Cover => _cell.Cover;

        public BasicCellBrain(ICell cell)
        {
            _cell = cell;
            
            InitStates();
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
            _currentState = state;
            _currentState.OnEnterState();
        }

        public void LiftFlag()
        {
            Flag.Lift();
        }

        public void PutDownFlag()
        {
            Flag.PutDown();
        }

        public Task<bool> LiftFlagAsync()
        {
            return Flag.LiftAsync();
        }

        public Task<bool> PutDownFlagAsync()
        {
            return Flag.PutDownAsync();
        }

        public void HideFlag()
        {
            Flag.Hide();
        }

        public void RevealCover()
        {
            Cover.Reveal();
        }

        public void OnLeftClick()
        {
            _currentState.OnLeftClick();
        }

        public void OnRightClick()
        {
            _currentState.OnRightLick();
        }
    }
}
