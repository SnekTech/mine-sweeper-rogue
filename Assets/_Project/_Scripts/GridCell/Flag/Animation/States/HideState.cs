using SnekTech.Core.Animation.CustomAnimator;

namespace SnekTech.GridCell.Flag
{
    public class HideState : FlagAnimState
    {
        public HideState(FlagAnimFSM flagAnimFSM, ICanAnimateSnek animContext, SnekAnimationClip clip) : base(
            flagAnimFSM, animContext, clip)
        {
        }

        public override void Lift()
        {
            flagAnimFSM.ChangeState(flagAnimFSM.LiftState);
        }

        public override void PutDown()
        {
        }
    }
}
