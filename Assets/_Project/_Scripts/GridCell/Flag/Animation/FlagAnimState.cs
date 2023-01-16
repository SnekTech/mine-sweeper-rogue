using SnekTech.Core.Animation;
using SnekTech.Core.Animation.CustomAnimator;

namespace SnekTech.GridCell.Flag
{
    public abstract class FlagAnimState : SpriteAnimState
    {
        protected readonly FlagAnimFSM flagAnimFSM;

        protected FlagAnimState(FlagAnimFSM flagAnimFSM, ICanAnimateSnek animContext, SnekAnimationClip clip) : base(
            animContext, clip)
        {
            this.flagAnimFSM = flagAnimFSM;
        }

        public abstract void Lift();
        public abstract void PutDown();
    }
}