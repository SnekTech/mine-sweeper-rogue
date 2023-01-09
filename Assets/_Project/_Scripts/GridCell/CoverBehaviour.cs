using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SnekTech.GridCell
{
    [RequireComponent(typeof(Animator))]
    public class CoverBehaviour : MonoBehaviour, ICover
    {
        public event Action RevealCompleted, PutCoverCompleted;

        private static readonly int RevealTrigger = Animator.StringToHash("Reveal");
        private static readonly int PutCoverTrigger = Animator.StringToHash("PutCover");
        private static readonly int InitPutCoverTrigger = Animator.StringToHash("InitPutCover");

        private Animator _animator;
        private UniTaskCompletionSource<bool> _revealCompletionSource = new UniTaskCompletionSource<bool>();
        private UniTaskCompletionSource<bool> _putCoverCompletionSource = new UniTaskCompletionSource<bool>();

        private UniTask<bool> OpenTask => _revealCompletionSource.Task;
        private UniTask<bool> CloseTask => _putCoverCompletionSource.Task;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            PutCoverAfterInit();
            
            _revealCompletionSource.TrySetResult(true);
            _putCoverCompletionSource.TrySetResult(true);
        }

        public void OnRevealComplete()
        {
            RevealCompleted?.Invoke();
            _revealCompletionSource.TrySetResult(true);
        }

        public void OnPutCoverComplete()
        {
            PutCoverCompleted?.Invoke();
            _putCoverCompletionSource.TrySetResult(true);
        }

        public bool IsActive
        {
            get => gameObject.activeSelf;
            set
            {
                gameObject.SetActive(value);
                PutCoverAfterInit();
            }
        }
        
        public UniTask<bool> RevealAsync()
        {
            if (OpenTask.IsPending())
            {
                return UniTask.FromResult(false);
            }

            _revealCompletionSource = new UniTaskCompletionSource<bool>();
            _animator.SetTrigger(RevealTrigger);
            return OpenTask;
        }

        public UniTask<bool> PutCoverAsync()
        {
            if (CloseTask.IsPending())
            {
                return UniTask.FromResult(false);
            }

            _putCoverCompletionSource = new UniTaskCompletionSource<bool>();
            _animator.SetTrigger(PutCoverTrigger);
            return CloseTask;
        }

        private void PutCoverAfterInit()
        {
            _animator.SetTrigger(InitPutCoverTrigger);
        }
    }
}
