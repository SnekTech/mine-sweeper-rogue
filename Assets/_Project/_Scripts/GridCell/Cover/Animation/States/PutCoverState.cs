using System;
using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Cover.Animation
{
    public class PutCoverState : CoverAnimState<SpriteClipNonLoop>
    {
        public event Action OnComplete;

        public PutCoverState(CoverAnimFSM animFSM, SpriteClipNonLoop clip) : base(animFSM, clip)
        {
        }

        public override void Enter()
        {
            base.Enter();

            spriteClip.OnComplete -= AnimComplete;
        }

        public override void Exit()
        {
            base.Exit();

            spriteClip.OnComplete -= AnimComplete;
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