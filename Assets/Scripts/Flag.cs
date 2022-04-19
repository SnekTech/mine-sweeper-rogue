using UnityEngine;

namespace SnekTech
{
    public class Flag : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int Growing = Animator.StringToHash("growing");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            // animator.SetBool(Growing, true);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
