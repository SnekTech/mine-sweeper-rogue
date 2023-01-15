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

        public Animator Animator { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        private FlagAnimFSM animFSM;

        private UniTaskCompletionSource<bool> _liftCompletionSource = new UniTaskCompletionSource<bool>();
        private UniTaskCompletionSource<bool> _putDownCompletionSource = new UniTaskCompletionSource<bool>();

        private UniTask<bool> LiftTask => _liftCompletionSource.Task;
        private UniTask<bool> PutDownTask => _putDownCompletionSource.Task;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();

            _liftCompletionSource.TrySetResult(true);
            _putDownCompletionSource.TrySetResult(true);

            animFSM = new FlagAnimFSM();
            animFSM.PopulateStates(
                new FloatState(animFSM, new SpriteClipLoop(this, animData.Float)),
                new HideState(animFSM, new SpriteClipLoop(this, animData.Hide)),
                new LiftState(animFSM, new SpriteClipNonLoop(this, animData.Lift)),
                new PutDownState(animFSM, new SpriteClipNonLoop(this, animData.PutDown))
            );

            animFSM.Init(animFSM.HideState);
            animFSM.Update();
        }

        private void OnEnable()
        {
            animFSM.LiftState.OnComplete += OnLiftAnimationComplete;
            animFSM.PutDownState.OnComplete += OnPutDownAnimationComplete;
        }

        private void OnDisable()
        {
            animFSM.LiftState.OnComplete -= OnLiftAnimationComplete;
            animFSM.PutDownState.OnComplete -= OnPutDownAnimationComplete;
        }

        private void OnLiftAnimationComplete()
        {
            LiftCompleted?.Invoke();
            _liftCompletionSource.TrySetResult(true);
        }

        private void OnPutDownAnimationComplete()
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

            animFSM.Triggers.ShouldLift = true;
            animFSM.Update();

            _liftCompletionSource = new UniTaskCompletionSource<bool>();
            return LiftTask;
        }

        public UniTask<bool> PutDownAsync()
        {
            if (PutDownTask.IsPending())
            {
                return UniTask.FromResult(false);
            }
            animFSM.Triggers.ShouldPutDown = true;
            animFSM.Update();

            _putDownCompletionSource = new UniTaskCompletionSource<bool>();
            return PutDownTask;
        }
    }
}