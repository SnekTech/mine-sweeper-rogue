using System;
using Cysharp.Threading.Tasks;
using SnekTech.Core.Animation;
using UnityEngine;

namespace SnekTech.GridCell.Flag
{
    [RequireComponent(typeof(Animator))]
    public class FlagBehaviour : MonoBehaviour, IFlag
    {
        public event Action LiftCompleted, PutDownCompleted;

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

            animFSM = new FlagAnimFSM();
            animFSM.PopulateStates(
                new FloatState(animFSM, new SpriteClipLoop(this, animData.Float)),
                new HideState(animFSM, new SpriteClipLoop(this, animData.Hide)),
                new LiftState(animFSM, new SpriteClipNonLoop(this, animData.Lift)),
                new PutDownState(animFSM, new SpriteClipNonLoop(this, animData.PutDown))
            );
            animFSM.Init(animFSM.HideState);
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
                LiftCompleted?.Invoke();
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
                PutDownCompleted?.Invoke();
                animFSM.PutDownState.OnComplete -= HandlePutDownComplete;
            }
            
            animFSM.PutDownState.OnComplete += HandlePutDownComplete;
            
            return putDownCompletionSource.Task;
        }
    }
}