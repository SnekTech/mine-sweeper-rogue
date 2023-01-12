namespace SnekTech.Core.FiniteStateMachine
{
    /// <summary>
    /// A basic <c>Finite State Machine(FSM)</c> class, works together with <see cref="State{T, TState, TMachine}"/>
    /// to implement the Finite State Machine pattern
    /// </summary>
    /// <typeparam name="T">context type, where the FSM lives in</typeparam>
    /// <typeparam name="TState">state type</typeparam>
    /// <typeparam name="TMachine">state machine type</typeparam>
    public class StateMachine<T, TState, TMachine>
        where TState : State<T, TState, TMachine>
        where TMachine : StateMachine<T, TState, TMachine>
    {
        public TState CurrentState { get; private set; }

        public void Init(TState initialState)
        {
            CurrentState = initialState;
            CurrentState.Enter();
        }

        public void ChangeState(TState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
