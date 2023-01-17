using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Flag
{
    public class FloatState : FlagAnimState
    {
        public FloatState(FlagAnimFSM flagAnimFSM, ICanAnimateSnek animContext, SnekAnimationClip clip) : base(
            flagAnimFSM, animContext, clip)
        {
        }

        public override void Lift()
        {
        }

        public override void PutDown()
        {
            flagAnimFSM.ChangeState(flagAnimFSM.PutDownState);
        }
    }
}
