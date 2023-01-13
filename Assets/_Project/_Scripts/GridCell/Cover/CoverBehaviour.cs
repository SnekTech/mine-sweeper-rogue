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

        // todo: move these to SO, create custom editor to set frameCount automatically
        private static readonly AnimInfo CoveredIdleAnimInfo = 
            new AnimInfo(Animator.StringToHash("cover-idle-covered"), 1);
        private static readonly AnimInfo RevealAnimInfo = 
            new AnimInfo(Animator.StringToHash("cover-reveal"), 38);
        private static readonly AnimInfo RevealedIdleAnimInfo = 
            new AnimInfo(Animator.StringToHash("cover-idle-revealed"), 1);
        private static readonly AnimInfo PutCoverAnimInfo = 
            new AnimInfo(Animator.StringToHash("cover-put-cover"), 2);
        public event Action RevealCompleted, PutCoverCompleted;

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
            CoveredIdleState = new CoveredIdleState(this, animFSM, new SpriteClipLoop(this, CoveredIdleAnimInfo));
            RevealState = new RevealState(this, animFSM, new SpriteClipNonLoop(this, RevealAnimInfo));
            RevealedIdleState = new RevealedIdleState(this, animFSM, new SpriteClipLoop(this, RevealedIdleAnimInfo));
            PutCoverState = new PutCoverState(this, animFSM, new SpriteClipNonLoop(this, PutCoverAnimInfo));
            
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