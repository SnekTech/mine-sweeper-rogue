using UnityEngine;

namespace SnekTech.Core.FiniteStateMachine
{
    public class FSM<T> : IFiniteStateMachine<T> where T : class, IState
    {
        protected T current;
        
        public void Init(T initialState)
        {
            current = initialState;
            current.Enter();
        }

        public void ChangeState(T newState)
        {
            Debug.Log($"{typeof(T).Name} {current.GetType().Name} ---> {current.GetType().Name}");
            if (newState == current) return;
            current.Exit();
            current = newState;
            current.Enter();
        }
    }
}
