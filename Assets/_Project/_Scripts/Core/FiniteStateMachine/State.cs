namespace SnekTech.Core.FiniteStateMachine
{
    /// <summary>
    /// Base abstract state, works together with <see cref="StateMachine{T, TState, TMachine}" />
    /// to implement the <c>Finite State Machine(FSM)</c> pattern
    /// </summary>
    /// <typeparam name="T">context type, where the FSM lives in, like a Player MonoBehavior</typeparam>
    /// <typeparam name="TState">state type</typeparam>
    /// <typeparam name="TMachine">state machine type</typeparam>
    public abstract class State<T, TState, TMachine> 
        where TState : State<T, TState, TMachine>
        where TMachine : StateMachine<T, TState, TMachine>
    {
        protected T context;
        protected readonly TMachine stateMachine;

        protected State(T context, TMachine stateMachine)
        {
            this.context = context;
            this.stateMachine = stateMachine;
        }
        
        public abstract void Enter();

        public abstract void Exit();

        public abstract void Update();
    }
}
