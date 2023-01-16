using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SnekTech.GridCell.Flag
{
    [RequireComponent(typeof(Animator))]
    public class FlagBehaviour : MonoBehaviour, IFlag
    {

        [SerializeField]
        private FlagAnimData animData;

        public bool IsActive
        {
            get => this.GetActiveSelf();
            set => this.SetActive(value);
        }

        public bool IsTransitioning => animFSM.IsInTransitionalState;

        public Animator Animator { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        private FlagAnimFSM animFSM;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();

            animFSM = new FlagAnimFSM(this, animData);
        }

        public UniTask<bool> LiftAsync()
        {
            if (animFSM.Current != animFSM.HideState)
            {
                return UniTask.FromResult(false);
            }

            animFSM.Lift();

            var liftCompletionSource = new UniTaskCompletionSource<bool>();

            void HandleListComplete()
            {
                liftCompletionSource.TrySetResult(true);
                animFSM.LiftState.OnComplete -= HandleListComplete;
            }

            animFSM.LiftState.OnComplete += HandleListComplete;
            
            return liftCompletionSource.Task;
        }

        public UniTask<bool> PutDownAsync()
        {
            if (animFSM.Current != animFSM.FloatState)
            {
                return UniTask.FromResult(false);
            }

            animFSM.PutDown();

            var putDownCompletionSource = new UniTaskCompletionSource<bool>();
            void HandlePutDownComplete()
            {
                putDownCompletionSource.TrySetResult(true);
                animFSM.PutDownState.OnComplete -= HandlePutDownComplete;
            }
            
            animFSM.PutDownState.OnComplete += HandlePutDownComplete;
            
            return putDownCompletionSource.Task;
        }
    }
}