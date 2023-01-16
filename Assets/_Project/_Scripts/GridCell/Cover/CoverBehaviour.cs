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

        [SerializeField]
        private CoverAnimData animData;

        public bool IsActive
        {
            get => this.GetActiveSelf();
            set => this.SetActive(value);
        }

        public Animator Animator { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        private CoverAnimFSM animFSM;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();

            animFSM = new CoverAnimFSM(this, animData);
        }
        
        public UniTask<bool> RevealAsync()
        {
            if (animFSM.IsInTransitionalState)
            {
                return UniTask.FromResult(false);
            }

            animFSM.Reveal();

            var revealCompletionSource = new UniTaskCompletionSource<bool>();

            void HandleRevealComplete()
            {
                revealCompletionSource.TrySetResult(true);
                animFSM.RevealState.OnComplete -= HandleRevealComplete;
                RevealCompleted?.Invoke();
            }

            animFSM.RevealState.OnComplete += HandleRevealComplete;
            
            return revealCompletionSource.Task;
        }

        public UniTask<bool> PutCoverAsync()
        {
            if (animFSM.IsInTransitionalState)
            {
                return UniTask.FromResult(false);
            }

            animFSM.PutCover();

            var putCoverCompletionSource = new UniTaskCompletionSource<bool>();

            void HandlePutCoverComplete()
            {
                putCoverCompletionSource.TrySetResult(true);
                animFSM.PutCoverState.OnComplete -= HandlePutCoverComplete;
                PutCoverCompleted?.Invoke();
            }

            animFSM.PutCoverState.OnComplete -= HandlePutCoverComplete;
            
            return putCoverCompletionSource.Task;
        }
    }
}