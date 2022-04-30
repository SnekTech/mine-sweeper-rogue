using System.Threading.Tasks;
using UnityEngine;

namespace SnekTech.GridCell
{
    [RequireComponent(typeof(Animator))]
    public class CoverBehaviour : MonoBehaviour, ICover
    {
        private Animator _animator;
        private static readonly int DisappearTrigger = Animator.StringToHash("Disappear");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void OnDisappearComplete()
        {
            gameObject.SetActive(false);
        }

        public bool IsActive
        {
            get => gameObject.activeInHierarchy;
            set => gameObject.SetActive(value);
        }
        
        public Task<bool> OpenAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CloseAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
