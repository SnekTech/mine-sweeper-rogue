using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Cover.Animation
{
    public class RevealedIdleState : CoverAnimState
    {
        public RevealedIdleState(CoverAnimFSM animFSM, ICanAnimateSnek animContext, SnekAnimationClip clip) :
            base(animFSM, animContext, clip)
        {
        }

        public override void Reveal()
        {
        }

        public override void PutCover()
        {
            coverAnimFSM.ChangeState(coverAnimFSM.PutCoverState);
        }
    }
}