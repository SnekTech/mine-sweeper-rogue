using System;
using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Cover.Animation
{
    public class RevealState : CoverAnimState<SpriteClipNonLoop>
    {
        public event Action OnComplete;
            
        public RevealState(ICoverAnimStateProvider coverAnimStateProvider, CoverAnimFSM animFSM, SpriteClipNonLoop spriteClipNonLoop) : 
            base(coverAnimStateProvider, animFSM, spriteClipNonLoop)
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
            animFSM.ChangeState(coverAnimStateProvider.RevealedIdleState);
            OnComplete?.Invoke();
        }
    }
}