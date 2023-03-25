using Cysharp.Threading.Tasks;
using SnekTech.Core.Animation;
using SnekTech.MineSweeperRogue.GridSystem.CellSystem;
using UnityEngine;

namespace SnekTech.GridCell.Flag
{
    public class Flag : MonoBehaviour, IFlag, ICanAnimateSnek
    {

        [SerializeField]
        private FlagAnimData animData;

        public SnekAnimator SnekAnimator { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        private FlagAnimFSM _animFSM;

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            SnekAnimator = GetComponent<SnekAnimator>();

            _animFSM = new FlagAnimFSM(this, animData);
        }

        public UniTask LiftAsync()
        {
            if (_animFSM.Current != _animFSM.HideState)
            {
                return UniTask.FromResult(false);
            }

            _animFSM.Current.Lift();

            var liftCompletionSource = new UniTaskCompletionSource<bool>();

            void HandleListComplete()
            {
                liftCompletionSource.TrySetResult(true);
                _animFSM.LiftState.OnComplete -= HandleListComplete;
            }

            _animFSM.LiftState.OnComplete += HandleListComplete;
            
            return liftCompletionSource.Task;
        }

        public UniTask PutDownAsync()
        {
            if (_animFSM.Current != _animFSM.FloatState)
            {
                return UniTask.FromResult(false);
            }

            _animFSM.Current.PutDown();

            var putDownCompletionSource = new UniTaskCompletionSource<bool>();
            void HandlePutDownComplete()
            {
                putDownCompletionSource.TrySetResult(true);
                _animFSM.PutDownState.OnComplete -= HandlePutDownComplete;
            }
            
            _animFSM.PutDownState.OnComplete += HandlePutDownComplete;
            
            return putDownCompletionSource.Task;
        }
    }
}