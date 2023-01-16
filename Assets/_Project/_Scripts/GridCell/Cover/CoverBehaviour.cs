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
            animFSM.PopulateStates(
                new CoveredIdleState(animFSM, new SpriteClipLoop(this, animData.CoveredIdle)),
                new RevealState(animFSM, new SpriteClipNonLoop(this, animData.Reveal)),
                new RevealedIdleState(animFSM, new SpriteClipLoop(this, animData.RevealedIdle)),
                new PutCoverState(animFSM, new SpriteClipNonLoop(this, animData.PutCover))
            );

            animFSM.Init(animFSM.CoveredIdleState);
        }

        private void OnEnable()
        {
            animFSM.RevealState.OnComplete += OnRevealComplete;
            animFSM.PutCoverState.OnComplete += OnPutCoverComplete;
        }

        private void OnDisable()
        {
            animFSM.RevealState.OnComplete -= OnRevealComplete;
            animFSM.PutCoverState.OnComplete -= OnPutCoverComplete;
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

            animFSM.Reveal();

            _revealCompletionSource = new UniTaskCompletionSource<bool>();
            return RevealTask;
        }

        public UniTask<bool> PutCoverAsync()
        {
            if (PutCoverTask.IsPending())
            {
                return UniTask.FromResult(false);
            }

            animFSM.PutCover();

            _putCoverCompletionSource = new UniTaskCompletionSource<bool>();
            return PutCoverTask;
        }
    }
}