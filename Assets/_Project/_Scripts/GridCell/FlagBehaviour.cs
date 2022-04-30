using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SnekTech.GridCell
{
    public class FlagBehaviour : MonoBehaviour, IFlag
    {
        public event Action LiftCompleted, PutDownCompleted;
        
        private static readonly int LiftTrigger = Animator.StringToHash("Lift");
        private static readonly int PutDownTrigger = Animator.StringToHash("PutDown");
        private static readonly int HideTrigger = Animator.StringToHash("Hide");

        private Animator _animator;
        private TaskCompletionSource<bool> _putDownCompletionSource = new TaskCompletionSource<bool>();

        private Task<bool> PutDownTask => _putDownCompletionSource.Task;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            
            _putDownCompletionSource.SetResult(true);
        }

        public void OnLiftAnimationComplete()
        {
            LiftCompleted?.Invoke();
        }

        public void OnPutDownAnimationComplete()
        {
            PutDownCompleted?.Invoke();
            _putDownCompletionSource.SetResult(true);
        }


        public void Lift()
        {
            _animator.SetTrigger(LiftTrigger);
        }

        public void PutDown()
        {
            _animator.SetTrigger(PutDownTrigger);
        }

        public void Hide()
        {
            _animator.SetTrigger(HideTrigger);
        }

        public Task<bool> PutDownAsync()
        {
            if (!PutDownTask.IsCompleted)
            {
                var tcs = new TaskCompletionSource<bool>();
                tcs.SetResult(false);
                return tcs.Task;
            }
            
            _putDownCompletionSource = new TaskCompletionSource<bool>();
            _animator.SetTrigger(PutDownTrigger);
            return PutDownTask;
        }
    }
}
