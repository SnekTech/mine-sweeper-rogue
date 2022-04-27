using System;
using UnityEngine;

namespace SnekTech.GridCell
{
    public class FlagBehaviour : MonoBehaviour
    {
        public event Action Disappeared;
        
        private Animator _animator;
        private static readonly int DisappearTrigger = Animator.StringToHash("Disappear");

        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void OnDisappearComplete()
        {
            Disappeared?.Invoke();
        }

        public void PutDown()
        {
            _animator.SetTrigger(DisappearTrigger);
        }

        public bool IsActive()
        {
            return gameObject.activeInHierarchy;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
