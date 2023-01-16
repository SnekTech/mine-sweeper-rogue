using SnekTech.Core.Animation;
using SnekTech.Core.FiniteStateMachine;

namespace SnekTech.GridCell.Flag
{
    public class FlagAnimFSM : FSM<IFlagAnimState>
    {
        public readonly FloatState FloatState;
        public readonly HideState HideState;
        public readonly LiftState LiftState;
        public readonly PutDownState PutDownState;
        
        public bool IsInTransitionalState => Current.IsTransitional;

        public FlagAnimFSM(ICanAnimate animContext, FlagAnimData animData)
        {
            FloatState = new FloatState(this, new SpriteClipLoop(animContext, animData.Float));
            HideState = new HideState(this, new SpriteClipLoop(animContext, animData.Hide));
            LiftState = new LiftState(this, new SpriteClipNonLoop(animContext, animData.Lift));
            PutDownState = new PutDownState(this, new SpriteClipNonLoop(animContext, animData.PutDown));
            
            Init(HideState);
        }
        
        public void Lift() => Current.Lift();
        public void PutDown() => Current.PutDown();
    }
}
