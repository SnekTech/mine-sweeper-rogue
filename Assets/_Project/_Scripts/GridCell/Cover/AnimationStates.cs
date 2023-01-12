namespace SnekTech.GridCell.Cover
{
    namespace Animation
    {
        public class CoveredIdleState : CoverAnimState
        {
            public CoveredIdleState(ICover context, CoverStateMachine stateMachine, int animHash, bool shouldLoop) : base(context, stateMachine, animHash, shouldLoop)
            {
            }

            public override void Exit()
            {
                base.Exit();

                stateMachine.Triggers.ShouldReveal = false;
            }

            public override void Update()
            {
                if (stateMachine.Triggers.ShouldReveal)
                {
                    stateMachine.ChangeState(context.RevealState);
                }
            }
        }
        
        public class RevealState : CoverAnimState
        {
            public RevealState(ICover context, CoverStateMachine stateMachine, int animHash, bool shouldLoop) : base(context, stateMachine, animHash, shouldLoop)
            {
            }

            public override void Enter()
            {
                base.Enter();

                OnComplete += HandleAnimComplete;
            }

            public override void Exit()
            {
                base.Exit();
                
                context.IsActive = false;
                OnComplete -= HandleAnimComplete;
            }

            public override void Update()
            {
            }

            private void HandleAnimComplete()
            {
                stateMachine.ChangeState(context.RevealedIdleState);
            }
        }
        
        public class RevealedIdleState : CoverAnimState
        {
            public RevealedIdleState(ICover context, CoverStateMachine stateMachine, int animHash, bool shouldLoop) : base(context, stateMachine, animHash, shouldLoop)
            {
            }

            public override void Exit()
            {
                base.Exit();

                context.IsActive = true;
            }

            public override void Update()
            {
                if (stateMachine.Triggers.ShouldPutCover)
                {
                    stateMachine.ChangeState(context.PutCoverState);
                }
            }
        }
        
        public class PutCoverState : CoverAnimState
        {
            public PutCoverState(ICover context, CoverStateMachine stateMachine, int animHash, bool shouldLoop) : base(context, stateMachine, animHash, shouldLoop)
            {
            }

            public override void Enter()
            {
                base.Enter();

                OnComplete += HandleAnimComplete;
            }

            public override void Exit()
            {
                base.Exit();

                stateMachine.Triggers.ShouldPutCover = false;
                OnComplete -= HandleAnimComplete;
            }

            public override void Update()
            {
            }

            private void HandleAnimComplete()
            {
                stateMachine.ChangeState(context.CoveredIdleState);
            }
        }
    }
}
