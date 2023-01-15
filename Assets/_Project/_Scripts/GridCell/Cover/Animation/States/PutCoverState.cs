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

            spriteClip.OnComplete -= HandleAnimComplete;
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
            animFSM.ChangeState(animFSM.CoveredIdleState);
            OnComplete?.Invoke();
        }
    }
}