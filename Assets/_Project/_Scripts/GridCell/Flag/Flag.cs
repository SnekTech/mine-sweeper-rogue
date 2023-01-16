using Cysharp.Threading.Tasks;
using SnekTech.Core.Animation.CustomAnimator;
using UnityEngine;

namespace SnekTech.GridCell.Flag
{
    [RequireComponent(typeof(Animator))]
    public class Flag : MonoBehaviour, IFlag
    {

        [SerializeField]
        private FlagAnimData animData;

        public SnekAnimator SnekAnimator { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        private FlagAnimFSM animFSM;

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            SnekAnimator = GetComponent<SnekAnimator>();

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