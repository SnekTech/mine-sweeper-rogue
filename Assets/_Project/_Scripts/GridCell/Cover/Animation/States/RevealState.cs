using System;
using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Cover.Animation
{
    public class RevealState : CoverAnimState
    {
        public event Action OnComplete;
            
        public RevealState(CoverAnimFSM animFSM, ICanAnimateSnek animContext, SnekAnimationClip clip) : 
            base(animFSM, animContext, clip)
        {
        }

        public override void Enter()
        {
            base.Enter();

            animator.OnClipComplete += AnimComplete;
        }

        public override void Exit()
        {
            base.Exit();

            animator.OnClipComplete -= AnimComplete;
        }

        private void AnimComplete()
        {
            coverAnimFSM.ChangeState(coverAnimFSM.RevealedIdleState);
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