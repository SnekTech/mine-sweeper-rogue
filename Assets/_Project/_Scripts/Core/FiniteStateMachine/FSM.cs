using UnityEngine;

namespace SnekTech.Core.FiniteStateMachine
{
    public class FSM<T> : IFiniteStateMachine<T> where T : class, IState
    {
        public T Current;
        
        public void Init(T initialState)
        {
            Current = initialState;
            Current.Enter();
        }

        public void ChangeState(T newState)
        {
            // Debug.Log($"{typeof(T).Name} {Current.GetType().Name} ---> {newState.GetType().Name}");
            if (newState == Current) return;
            Current.Exit();
            Current = newState;
            Current.Enter();
        }
    }
}
