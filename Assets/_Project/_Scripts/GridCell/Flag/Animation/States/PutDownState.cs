using System;
using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Flag
{
    public class PutDownState : FlagAnimState<SpriteClipNonLoop>
    {
        public event Action OnComplete;
        
        public PutDownState(FlagAnimFSM flagAnimFSM, SpriteClipNonLoop spriteClip) : base(flagAnimFSM, spriteClip)
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
            animFSM.ChangeState(animFSM.HideState);
            OnComplete?.Invoke();
        }
    }
}
