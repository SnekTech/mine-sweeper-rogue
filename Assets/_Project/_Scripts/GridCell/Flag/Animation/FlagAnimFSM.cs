using SnekTech.Core.FiniteStateMachine;

namespace SnekTech.GridCell.Flag
{
    public class FlagAnimFSM : FSM<FlagAnimState>
    {
        public readonly FloatState FloatState;
        public readonly HideState HideState;
        public readonly LiftState LiftState;
        public readonly PutDownState PutDownState;
        
        public FlagAnimFSM(ICanAnimateSnek animContext, FlagAnimData animData)
        {
            FloatState = new FloatState(this, animContext, animData.Float);
            HideState = new HideState(this, animContext, animData.Hide);
            LiftState = new LiftState(this, animContext, animData.Lift);
            PutDownState = new PutDownState(this, animContext, animData.PutDown);
            
            Init(HideState);
        }
    }
}
