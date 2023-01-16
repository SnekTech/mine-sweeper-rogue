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

            spriteClip.OnComplete += AnimComplete;
        }

        public override void Exit()
        {
            base.Exit();

            spriteClip.OnComplete -= AnimComplete;
        }

        private void AnimComplete()
        {
            flagAnimFSM.ChangeState(flagAnimFSM.FloatState);
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
