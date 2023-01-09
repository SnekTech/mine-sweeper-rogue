using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SnekTech.GridCell
{
    [RequireComponent(typeof(Animator))]
    public class FlagBehaviour : MonoBehaviour, IFlag
    {
        public event Action LiftCompleted, PutDownCompleted;
        
        private static readonly int LiftTrigger = Animator.StringToHash("Lift");
        private static readonly int PutDownTrigger = Animator.StringToHash("PutDown");

        private Animator _animator;
        
        private UniTaskCompletionSource<bool> _liftCompletionSource = new UniTaskCompletionSource<bool>();
        private UniTaskCompletionSource<bool> _putDownCompletionSource = new UniTaskCompletionSource<bool>();

        private UniTask<bool> LiftTask => _liftCompletionSource.Task;
        private UniTask<bool> PutDownTask => _putDownCompletionSource.Task;


        public bool IsActive
        {
            get => this.GetActiveSelf();
            set => this.SetActive(value);
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            
            _liftCompletionSource.TrySetResult(true);
            _putDownCompletionSource.TrySetResult(true);
        }

        public void OnLiftAnimationComplete()
        {
            LiftCompleted?.Invoke();
            _liftCompletionSource.TrySetResult(true);
        }

        public void OnPutDownAnimationComplete()
        {
            PutDownCompleted?.Invoke();
            _putDownCompletionSource.TrySetResult(true);
        }

        public UniTask<bool> LiftAsync()
        {
            if (LiftTask.IsPending())
            {
                return UniTask.FromResult(false);
            }

            _liftCompletionSource = new UniTaskCompletionSource<bool>();
            _animator.SetTrigger(LiftTrigger);
            return LiftTask;
        }

        public UniTask<bool> PutDownAsync()
        {
            if (PutDownTask.IsPending())
            {
                return UniTask.FromResult(false);
            }
            
            _putDownCompletionSource = new UniTaskCompletionSource<bool>();
            _animator.SetTrigger(PutDownTrigger);
            return PutDownTask;
        }
    }
}
