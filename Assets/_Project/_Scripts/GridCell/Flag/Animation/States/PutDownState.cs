using System;
using SnekTech.Core.Animation;

namespace SnekTech.GridCell.Flag
{
    public class PutDownState : FlagAnimState
    {
        public event Action OnComplete;

        public PutDownState(FlagAnimFSM flagAnimFSM, ICanAnimateSnek animContext, SnekAnimationClip clip) : base(flagAnimFSM, animContext, clip)
        {
        }
        
        public override void Enter()
        {
            base.Enter();

            animator.OnClipComplete += HandleAnimComplete;
        }

        public override void Exit()
        {
            base.Exit();
            
            animator.OnClipComplete -= HandleAnimComplete;
        }

        private void HandleAnimComplete()
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
