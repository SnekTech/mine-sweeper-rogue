using System;
using UnityEngine;

namespace SnekTech
{
    public class Flag : MonoBehaviour
    {
        public event Action DisappearCompleted;
        
        private Animator _animator;
        private static readonly int DisappearTrigger = Animator.StringToHash("Disappear");

        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void OnDisappearComplete()
        {
            DisappearCompleted?.Invoke();
        }

        public void PutDown()
        {
            _animator.SetTrigger(DisappearTrigger);
        }
    }
}
