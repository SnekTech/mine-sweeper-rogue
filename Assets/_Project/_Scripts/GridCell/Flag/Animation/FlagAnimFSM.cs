using SnekTech.Core.FiniteStateMachine;
using UnityEngine;

namespace SnekTech.GridCell.Flag
{
    public class FlagAnimFSM : IFiniteStateMachine<IFlagAnimState>
    {
        public IFlagAnimState Current { get; private set; }

        public FloatState FloatState { get; private set; }
        public HideState HideState { get; private set; }
        public LiftState LiftState { get; private set; }
        public PutDownState PutDownState { get; private set; }

        public bool IsInTransitionalState => Current.IsTransitional;

        public void PopulateStates(FloatState floatState, HideState hideState, LiftState liftState, PutDownState putDownState)
        {
            FloatState = floatState;
            HideState = hideState;
            LiftState = liftState;
            PutDownState = putDownState;
        }

        public void Init(IFlagAnimState initialState)
        {
            Current = initialState;
            Current.Enter();
        }

        public void ChangeState(IFlagAnimState newState)
        {
            Debug.Log($"{nameof(FlagAnimFSM)} {Current} ---> {newState}");
            Current.Exit();
            Current = newState;
            Current.Enter();
        }

        public void Lift() => Current.Lift();
        public void PutDown() => Current.PutDown();
    }
}
