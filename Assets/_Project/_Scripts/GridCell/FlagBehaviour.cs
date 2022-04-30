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
        
        private TaskCompletionSource<bool> _liftCompletionSource = new TaskCompletionSource<bool>();
        private TaskCompletionSource<bool> _putDownCompletionSource = new TaskCompletionSource<bool>();

        private Task<bool> LiftTask => _liftCompletionSource.Task;
        private Task<bool> PutDownTask => _putDownCompletionSource.Task;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            
            _liftCompletionSource.SetResult(true);
            _putDownCompletionSource.SetResult(true);
        }

        public void OnLiftAnimationComplete()
        {
            LiftCompleted?.Invoke();
            _liftCompletionSource.SetResult(true);
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

        public Task<bool> LiftAsync()
        {
            if (!LiftTask.IsCompleted)
            {
                return Utils.GetCompletedTask(false);
            }

            _liftCompletionSource = new TaskCompletionSource<bool>();
            _animator.SetTrigger(LiftTrigger);
            return LiftTask;
        }

        public Task<bool> PutDownAsync()
        {
            if (!PutDownTask.IsCompleted)
            {
                return Utils.GetCompletedTask(false);
            }
            
            _putDownCompletionSource = new TaskCompletionSource<bool>();
            _animator.SetTrigger(PutDownTrigger);
            return PutDownTask;
        }
    }
}
