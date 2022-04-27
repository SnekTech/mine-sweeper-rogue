using System;
using UnityEngine;

namespace SnekTech.GridCell
{
    public class FlagBehaviour : MonoBehaviour, IFlag
    {
        public event Action LiftCompleted, PutDownCompleted;
        
        private static readonly int LiftTrigger = Animator.StringToHash("Lift");
        private static readonly int DisappearTrigger = Animator.StringToHash("PutDown");

        private Flag _flag;
        private Animator _animator;

        public bool IsActive => gameObject.activeInHierarchy;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _flag = new Flag(this);
        }

        public void OnLiftAnimationComplete()
        {
            LiftCompleted?.Invoke();
        }

        public void OnPutDownAnimationComplete()
        {
            PutDownCompleted?.Invoke();
        }


        public void Lift()
        {
            gameObject.SetActive(true);
            _animator.SetTrigger(LiftTrigger);
        }

        public void PutDown()
        {
            _animator.SetTrigger(DisappearTrigger);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
