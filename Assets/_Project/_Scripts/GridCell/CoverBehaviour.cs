using System;
using System.Threading.Tasks;
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

        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private TaskCompletionSource<bool> _revealCompletionSource = new TaskCompletionSource<bool>();
        private TaskCompletionSource<bool> _putCoverCompletionSource = new TaskCompletionSource<bool>();

        private Task<bool> OpenTask => _revealCompletionSource.Task;
        private Task<bool> CloseTask => _putCoverCompletionSource.Task;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            PutCoverAfterInit();
            
            _revealCompletionSource.SetResult(true);
            _putCoverCompletionSource.SetResult(true);
        }

        public void OnRevealComplete()
        {
            RevealCompleted?.Invoke();
            _revealCompletionSource.SetResult(true);
        }

        public void OnPutCoverComplete()
        {
            PutCoverCompleted?.Invoke();
            _putCoverCompletionSource.SetResult(true);
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
        
        public Task<bool> RevealAsync()
        {
            if (!OpenTask.IsCompleted)
            {
                return Task.FromResult(false);
            }

            _revealCompletionSource = new TaskCompletionSource<bool>();
            _animator.SetTrigger(RevealTrigger);
            return OpenTask;
        }

        public Task<bool> PutCoverAsync()
        {
            if (!CloseTask.IsCompleted)
            {
                return Task.FromResult(false);
            }

            _putCoverCompletionSource = new TaskCompletionSource<bool>();
            _animator.SetTrigger(PutCoverTrigger);
            return CloseTask;
        }

        private void PutCoverAfterInit()
        {
            _animator.SetTrigger(InitPutCoverTrigger);
        }

        public void SetHighlight(bool isHighlight)
        {
            _spriteRenderer.color = isHighlight ? Color.red : Color.white;
        }
    }
}
