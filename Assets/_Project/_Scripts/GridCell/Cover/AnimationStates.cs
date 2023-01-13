using System;
using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Cover
{
    namespace Animation
    {
        public class CoveredIdleState : CoverAnimState<SpriteClipLoop>
        {
            public CoveredIdleState(ICover cover, CoverAnimFSM animFSM, SpriteClipLoop spriteClipLoop) :
                base(cover, animFSM, spriteClipLoop)
            {
            }

            public override void Exit()
            {
                base.Exit();

                animFSM.Triggers.ShouldReveal = false;
            }

            public override void Update()
            {
                if (animFSM.Triggers.ShouldReveal)
                {
                    animFSM.ChangeState(cover.RevealState);
                }
            }
        }

        public class RevealState : CoverAnimState<SpriteClipNonLoop>
        {
            public event Action OnComplete;
            
            public RevealState(ICover cover, CoverAnimFSM animFSM, SpriteClipNonLoop spriteClipNonLoop) : 
                base(cover, animFSM, spriteClipNonLoop)
            {
            }

            public override void Enter()
            {
                base.Enter();

                spriteClip.OnComplete += HandleAnimComplete;
            }

            public override void Exit()
            {
                base.Exit();

                spriteClip.OnComplete -= HandleAnimComplete;
            }

            public override void Update()
            {
            }

            private void HandleAnimComplete()
            {
                animFSM.ChangeState(cover.RevealedIdleState);
                OnComplete?.Invoke();
            }
        }

        public class RevealedIdleState : CoverAnimState<SpriteClipLoop>
        {
            public RevealedIdleState(ICover cover, CoverAnimFSM animFSM, SpriteClipLoop spriteClipLoop) :
                base(cover, animFSM, spriteClipLoop)
            {
            }

            public override void Exit()
            {
                base.Exit();

                cover.IsActive = true;
            }

            public override void Update()
            {
                if (animFSM.Triggers.ShouldPutCover)
                {
                    animFSM.ChangeState(cover.PutCoverState);
                }
            }
        }

        public class PutCoverState : CoverAnimState<SpriteClipNonLoop>
        {
            public event Action OnComplete;
            
            public PutCoverState(ICover context, CoverAnimFSM animFSM, SpriteClipNonLoop clip) : base(
                context, animFSM, clip)
            {
            }

            public override void Enter()
            {
                base.Enter();

                spriteClip.OnComplete -= HandleAnimComplete;
            }

            public override void Exit()
            {
                base.Exit();

                animFSM.Triggers.ShouldPutCover = false;
                
                spriteClip.OnComplete -= HandleAnimComplete;
            }

            public override void Update()
            {
            }

            private void HandleAnimComplete()
            {
                animFSM.ChangeState(cover.CoveredIdleState);
                OnComplete?.Invoke();
            }
        }
    }
}