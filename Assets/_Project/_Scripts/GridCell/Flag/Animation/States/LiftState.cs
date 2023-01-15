using System;
using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Flag
{
    public class LiftState : FlagAnimState<SpriteClipNonLoop>
    {
        public event Action OnComplete;
        
        public LiftState(FlagAnimFSM flagAnimFSM, SpriteClipNonLoop spriteClip) : base(flagAnimFSM, spriteClip)
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
            animFSM.ChangeState(animFSM.FloatState);
            OnComplete?.Invoke();
        }
    }
}
