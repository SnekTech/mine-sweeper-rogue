using System;
using UnityEngine;

namespace SnekTech.GridCell
{
    public class FlagBehaviour : MonoBehaviour, IFlag
    {
        public event Action LiftCompleted, PutDownCompleted;
        
        private static readonly int LiftTrigger = Animator.StringToHash("Lift");
        private static readonly int DisappearTrigger = Animator.StringToHash("PutDown");
        private static readonly int HideTrigger = Animator.StringToHash("Hide");

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
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
            _animator.SetTrigger(LiftTrigger);
        }

        public void PutDown()
        {
            _animator.SetTrigger(DisappearTrigger);
        }

        public void Hide()
        {
            _animator.SetTrigger(HideTrigger);
        }
    }
}
