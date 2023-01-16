using System;
using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Cover.Animation
{
    public class RevealState : CoverAnimState<SpriteClipNonLoop>
    {
        public event Action OnComplete;
            
        public RevealState(CoverAnimFSM animFSM, SpriteClipNonLoop spriteClipNonLoop) : 
            base(animFSM, spriteClipNonLoop)
        {
        }

        public override void Enter()
        {
            base.Enter();

            spriteClip.OnComplete += AnimComplete;
        }

        public override void Exit()
        {
            base.Exit();

            spriteClip.OnComplete -= AnimComplete;
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