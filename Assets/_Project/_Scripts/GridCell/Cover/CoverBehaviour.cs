using System;
using Cysharp.Threading.Tasks;
using SnekTech.Core.Animation;
using SnekTech.GridCell.Cover.Animation;
using UnityEngine;

namespace SnekTech.GridCell.Cover
{
    [RequireComponent(typeof(Animator))]
    public class CoverBehaviour : MonoBehaviour, ICover
    {
        public event Action RevealCompleted, PutCoverCompleted;

        [SerializeField]
        private CoverAnimData animData;
        
        public bool IsActive
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public Animator Animator { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        
        public CoveredIdleState CoveredIdleState { get; private set; }
        public RevealState RevealState { get; private set; }
        public RevealedIdleState RevealedIdleState { get; private set; }
        public PutCoverState PutCoverState { get; private set; }

        private CoverAnimFSM animFSM;
        
        private UniTaskCompletionSource<bool> _revealCompletionSource = new UniTaskCompletionSource<bool>();
        private UniTaskCompletionSource<bool> _putCoverCompletionSource = new UniTaskCompletionSource<bool>();

        private UniTask<bool> RevealTask => _revealCompletionSource.Task;
        private UniTask<bool> PutCoverTask => _putCoverCompletionSource.Task;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();

            _revealCompletionSource.TrySetResult(true);
            _putCoverCompletionSource.TrySetResult(true);

            animFSM = new CoverAnimFSM();
            CoveredIdleState = new CoveredIdleState(this, animFSM, new SpriteClipLoop(this, animData.CoveredIdle));
            RevealState = new RevealState(this, animFSM, new SpriteClipNonLoop(this, animData.Reveal));
            RevealedIdleState = new RevealedIdleState(this, animFSM, new SpriteClipLoop(this, animData.RevealedIdle));
            PutCoverState = new PutCoverState(this, animFSM, new SpriteClipNonLoop(this, animData.PutCover));
            
            animFSM.Init(CoveredIdleState);
            animFSM.CurrentState.Update();
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

            animFSM.Triggers.ShouldReveal = true;
            animFSM.CurrentState.Update();

            _revealCompletionSource = new UniTaskCompletionSource<bool>();
            return RevealTask;
        }

        public UniTask<bool> PutCoverAsync()
        {
            if (PutCoverTask.IsPending())
            {
                return UniTask.FromResult(false);
            }

            animFSM.Triggers.ShouldPutCover = true;
            animFSM.CurrentState.Update();

            _putCoverCompletionSource = new UniTaskCompletionSource<bool>();
            return PutCoverTask;
        }
    }
}