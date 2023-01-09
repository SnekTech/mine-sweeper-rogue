using Cysharp.Threading.Tasks;

namespace SnekTech.GridCell
{
    public class BasicCellBrain : ICellBrain
    {
        public CellState CoveredState { get; private set; }
        public CellState FlaggedState { get; private set; }
        public CellState RevealedState { get; private set; }

        private readonly ICell _cell;
        private CellState _currentState;

        public ICell Cell => _cell;

        public bool IsFlagged => _currentState == FlaggedState;
        public bool IsCovered => _currentState == CoveredState;
        public bool IsRevealed => _currentState == RevealedState;

        public BasicCellBrain(ICell cell)
        {
            _cell = cell;
            
            GenerateStates();
            Init();
        }

        private void GenerateStates()
        {
            CoveredState ??= new CellCoveredState(this);
            FlaggedState ??= new CellFlaggedState(this);
            RevealedState ??= new CellRevealedState(this);
        }

        private void Init()
        {
            SwitchState(CoveredState);
        }

        public void SwitchState(CellState state)
        {
            _currentState?.OnLeaveState();
            _currentState = state;
            _currentState.OnEnterState();
        }

        public UniTask<bool> OnLeftClick()
        {
            return _currentState.OnLeftClick();
        }

        public UniTask<bool> OnRightClick()
        {
            return _currentState.OnRightLick();
        }
    }
}
