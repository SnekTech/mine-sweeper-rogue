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

            spriteClip.OnComplete += AnimComplete;
        }

        public override void Exit()
        {
            base.Exit();
            
            spriteClip.OnComplete -= AnimComplete;
        }

        private void AnimComplete()
        {
            flagAnimFSM.ChangeState(flagAnimFSM.HideState);
            OnComplete?.Invoke();
        }

        public override void Lift()
        {
        }

        public override void PutDown()
        {
        }
    }
}
