using System;
using SnekTech.Core.Animation.CustomAnimator;

namespace SnekTech.GridCell.Cover.Animation
{
    public class PutCoverState : CoverAnimState
    {
        public event Action OnComplete;
        public PutCoverState(CoverAnimFSM animFSM, ICanAnimateSnek animContext, SnekAnimationClip clip) :
            base(animFSM, animContext, clip)
        {
        }

        public override void Enter()
        {
            base.Enter();

            animator.OnClipComplete -= AnimComplete;
        }

        public override void Exit()
        {
            base.Exit();

            animator.OnClipComplete -= AnimComplete;
        }

        private void AnimComplete()
        {
            coverAnimFSM.ChangeState(coverAnimFSM.CoveredIdleState);
            OnComplete?.Invoke();
        }

        public override void Reveal()
        {
        }

        public override void PutCover()
        {
        }
    }
}