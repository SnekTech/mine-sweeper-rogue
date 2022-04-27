using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnekTech
{
    [RequireComponent(typeof(Animator))]
    public class Cover : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int DisappearTrigger = Animator.StringToHash("Disappear");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Reveal()
        {
            _animator.SetTrigger(DisappearTrigger);
        }

        public void OnDisappearComplete()
        {
            gameObject.SetActive(false);
        }
    }
}
