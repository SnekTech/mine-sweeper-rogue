using System.Threading.Tasks;
using UnityEngine;

namespace SnekTech.GridCell
{
    [RequireComponent(typeof(Animator))]
    public class CoverBehaviour : MonoBehaviour, ICover
    {
        private static readonly int RevealTrigger = Animator.StringToHash("Reveal");
        private static readonly int PutCoverTrigger = Animator.StringToHash("PutCover");
        private static readonly int InitPutCoverTrigger = Animator.StringToHash("InitPutCover");
        
        private Animator _animator;
        private TaskCompletionSource<bool> _openCompletionSource = new TaskCompletionSource<bool>();
        private TaskCompletionSource<bool> _closeCompletionSource = new TaskCompletionSource<bool>();

        private Task<bool> OpenTask => _openCompletionSource.Task;
        private Task<bool> CloseTask => _closeCompletionSource.Task;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            PutCoverAfterInit();
            
            _openCompletionSource.SetResult(true);
            _closeCompletionSource.SetResult(true);
        }

        public void OnOpenComplete()
        {
            _openCompletionSource.SetResult(true);
        }

        public void OnCloseComplete()
        {
            _closeCompletionSource.SetResult(true);
        }

        public bool IsActive
        {
            get => gameObject.activeInHierarchy;
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
                return Utils.GetCompletedTask(false);
            }

            _openCompletionSource = new TaskCompletionSource<bool>();
            _animator.SetTrigger(RevealTrigger);
            return OpenTask;
        }

        public Task<bool> PutCoverAsync()
        {
            if (!CloseTask.IsCompleted)
            {
                return Utils.GetCompletedTask(false);
            }

            _closeCompletionSource = new TaskCompletionSource<bool>();
            _animator.SetTrigger(PutCoverTrigger);
            return CloseTask;
        }

        private void PutCoverAfterInit()
        {
            _animator.SetTrigger(InitPutCoverTrigger);
        }
    }
}
