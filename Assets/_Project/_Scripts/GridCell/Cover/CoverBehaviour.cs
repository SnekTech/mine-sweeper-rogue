using System;
using Cysharp.Threading.Tasks;
using SnekTech.GridCell.Cover.Animation;
using UnityEngine;

namespace SnekTech.GridCell.Cover
{
    [RequireComponent(typeof(Animator))]
    public class CoverBehaviour : MonoBehaviour, ICover
    {
        public event Action RevealCompleted, PutCoverCompleted;

        public bool IsActive
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public Animator Animator => _animator;
        
        public CoveredIdleState CoveredIdleState { get; private set; }
        public RevealState RevealState { get; private set; }
        public RevealedIdleState RevealedIdleState { get; private set; }
        public PutCoverState PutCoverState { get; private set; }
        

        private static readonly int CoveredIdleAnim = Animator.StringToHash("cover-idle-covered");
        private static readonly int RevealAnim = Animator.StringToHash("cover-reveal");
        private static readonly int RevealedIdleAnim = Animator.StringToHash("cover-idle-revealed");
        private static readonly int PutCoverAnim = Animator.StringToHash("cover-put-cover");

        private Animator _animator;
        private CoverStateMachine _stateMachine;
        
        private UniTaskCompletionSource<bool> _revealCompletionSource = new UniTaskCompletionSource<bool>();
        private UniTaskCompletionSource<bool> _putCoverCompletionSource = new UniTaskCompletionSource<bool>();

        private UniTask<bool> RevealTask => _revealCompletionSource.Task;
        private UniTask<bool> PutCoverTask => _putCoverCompletionSource.Task;

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            _revealCompletionSource.TrySetResult(true);
            _putCoverCompletionSource.TrySetResult(true);

            _stateMachine = new CoverStateMachine();
            CoveredIdleState = new CoveredIdleState(this, _stateMachine, CoveredIdleAnim, true);
            RevealState = new RevealState(this, _stateMachine, RevealAnim, false);
            RevealedIdleState = new RevealedIdleState(this, _stateMachine, RevealedIdleAnim, true);
            PutCoverState = new PutCoverState(this, _stateMachine, PutCoverAnim, false);
            
            _stateMachine.Init(CoveredIdleState);
            _stateMachine.CurrentState.Update();
        }

        private void OnEnable()
        {
            RevealState.OnComplete += OnRevealComplete;
            PutCoverState.OnComplete += OnPutCoverComplete;
        }

        private void OnDisable()
        {
            RevealState.OnComplete -= OnRevealComplete;
            PutCoverState.OnComplete -= OnPutCoverComplete;
        }

        private void OnRevealComplete()
        {
            RevealCompleted?.Invoke();
            _revealCompletionSource.TrySetResult(true);
        }

        private void OnPutCoverComplete()
        {
            PutCoverCompleted?.Invoke();
            _putCoverCompletionSource.TrySetResult(true);
        }

        public UniTask<bool> RevealAsync()
        {
            if (RevealTask.IsPending())
            {
                return UniTask.FromResult(false);
            }

            _stateMachine.Triggers.ShouldReveal = true;
            _stateMachine.CurrentState.Update();

            _revealCompletionSource = new UniTaskCompletionSource<bool>();
            return RevealTask;
        }

        public UniTask<bool> PutCoverAsync()
        {
            if (PutCoverTask.IsPending())
            {
                return UniTask.FromResult(false);
            }

            _stateMachine.Triggers.ShouldPutCover = true;
            _stateMachine.CurrentState.Update();

            _putCoverCompletionSource = new UniTaskCompletionSource<bool>();
            return PutCoverTask;
        }
    }
}