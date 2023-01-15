using System;
using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Flag
{
    public struct Triggers
    {
        public bool ShouldLift;

        public bool ShouldPutDown;
    }
    
    public class FlagAnimFSM : SpriteAnimFSM
    {
        public Triggers Triggers = new Triggers
        {
            ShouldLift = false,
            ShouldPutDown = false,
        };

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
    }
}
