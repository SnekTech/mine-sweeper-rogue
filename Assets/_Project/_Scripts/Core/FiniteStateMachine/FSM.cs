using UnityEngine;

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
            // Debug.Log($"changing from {CurrentState} to {newState}");
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void Update()
        {
            CurrentState.Update();
        }
    }
}
