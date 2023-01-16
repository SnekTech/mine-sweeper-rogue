using SnekTech.Core.Animation;
using SnekTech.Core.FiniteStateMachine;

namespace SnekTech.GridCell.Flag
{
    public class FlagAnimFSM : IFiniteStateMachine<IFlagAnimState>
    {
        private IFlagAnimState _current;
        
        public FloatState FloatState { get; private set; }
        public HideState HideState { get; private set; }
        public LiftState LiftState { get; private set; }
        public PutDownState PutDownState { get; private set; }

        public void PopulateStates(FloatState floatState, HideState hideState, LiftState liftState, PutDownState putDownState)
        {
            FloatState = floatState;
            HideState = hideState;
            LiftState = liftState;
            PutDownState = putDownState;
        }

        public void Init(IFlagAnimState initialState)
        {
            _current = initialState;
            _current.Enter();
        }

        public void ChangeState(IFlagAnimState newState)
        {
            _current.Exit();
            _current = newState;
            _current.Enter();
        }

        public void Lift() => _current.Lift();
        public void PutDown() => _current.PutDown();
    }
}
