using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SnekTech.Core.FiniteStateMachine.Animation
{
    public abstract class AnimationState<T, TState, TMachine> : State<T, TState, TMachine>
        where T : ICanAnimate, ICanSwitchActiveness
        where TState : State<T, TState, TMachine>
        where TMachine : StateMachine<T, TState, TMachine>
    {
        public event Action OnComplete;
        
        private readonly int emptyAnimHash = Animator.StringToHash("");
        
        private readonly Animator _animator;
        private readonly int _animHash;
        private readonly bool _shouldLoop;

        private float _startTime;

        protected AnimationState(T context, TMachine stateMachine, int animHash, bool shouldLoop) : base(context, stateMachine)
        {
            _animator = this.context.Animator;
            _animHash = animHash;
            _shouldLoop = shouldLoop;
        }

        public override void Enter()
        {
            Play(_animHash);
            
            if (!_shouldLoop)
            {
                BeginWaitForAnimComplete().Forget();
            }
        }

        public override void Exit()
        {
        }

        private void Play(int animHash)
        {
            if (!context.IsActive || animHash == emptyAnimHash)
            {
                return;
            }
            
            int currentPlayingAnimHash = _animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
            if (animHash == currentPlayingAnimHash)
            {
                return;
            }
            
            _animator.Play(animHash);
        }

        private async UniTaskVoid BeginWaitForAnimComplete()
        {
            await UniTask.WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
            OnComplete?.Invoke();
        }
    }
}
