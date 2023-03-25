using Cysharp.Threading.Tasks;
using SnekTech.Core.Animation;
using SnekTech.GridCell.Cover.Animation;
using SnekTech.MineSweeperRogue.GridSystem.CellSystem;
using UnityEngine;

namespace SnekTech.GridCell.Cover
{
    public class Cover : MonoBehaviour, ICover, ICanAnimateSnek
    {
        [SerializeField]
        private CoverAnimData animData;

        public SnekAnimator SnekAnimator { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        private CoverAnimFSM _animFSM;

        private void Awake()
        {
            SnekAnimator = GetComponent<SnekAnimator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();

            _animFSM = new CoverAnimFSM(this, animData);
        }
        
        public UniTask RevealAsync()
        {
            if (_animFSM.Current != _animFSM.CoveredIdleState)
            {
                return UniTask.FromResult(false);
            }

            _animFSM.Reveal();

            var revealCompletionSource = new UniTaskCompletionSource<bool>();

            void HandleRevealComplete()
            {
                revealCompletionSource.TrySetResult(true);
                _animFSM.RevealState.OnComplete -= HandleRevealComplete;
            }

            _animFSM.RevealState.OnComplete += HandleRevealComplete;
            
            return revealCompletionSource.Task;
        }

        public UniTask PutCoverAsync()
        {
            if (_animFSM.Current != _animFSM.RevealedIdleState)
            {
                return UniTask.FromResult(false);
            }

            _animFSM.PutCover();

            var putCoverCompletionSource = new UniTaskCompletionSource<bool>();

            void HandlePutCoverComplete()
            {
                putCoverCompletionSource.TrySetResult(true);
                _animFSM.PutCoverState.OnComplete -= HandlePutCoverComplete;
            }

            _animFSM.PutCoverState.OnComplete -= HandlePutCoverComplete;
            
            return putCoverCompletionSource.Task;
        }
    }
}