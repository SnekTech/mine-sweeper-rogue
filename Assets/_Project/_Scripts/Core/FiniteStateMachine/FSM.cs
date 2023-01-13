namespace SnekTech.Core.FiniteStateMachine
{
    public class FSM
    {
        public State CurrentState { get; private set; }

        public void Init(State initialState)
        {
            CurrentState = initialState;
            CurrentState.Enter();
        }

        public void ChangeState(State newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
