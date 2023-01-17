using Cysharp.Threading.Tasks;
using SnekTech.Core.Animation;
using SnekTech.GridCell.Cover.Animation;
using UnityEngine;

namespace SnekTech.GridCell.Cover
{
    public class Cover : MonoBehaviour, ICover
    {
        [SerializeField]
        private CoverAnimData animData;

        public SnekAnimator SnekAnimator { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        private CoverAnimFSM animFSM;

        private void Awake()
        {
            SnekAnimator = GetComponent<SnekAnimator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();

            animFSM = new CoverAnimFSM(this, animData);
        }
        
        public UniTask<bool> RevealAsync()
        {
            if (animFSM.Current != animFSM.CoveredIdleState)
            {
                return UniTask.FromResult(false);
            }

            animFSM.Reveal();

            var revealCompletionSource = new UniTaskCompletionSource<bool>();

            void HandleRevealComplete()
            {
                revealCompletionSource.TrySetResult(true);
                animFSM.RevealState.OnComplete -= HandleRevealComplete;
            }

            animFSM.RevealState.OnComplete += HandleRevealComplete;
            
            return revealCompletionSource.Task;
        }

        public UniTask<bool> PutCoverAsync()
        {
            if (animFSM.Current != animFSM.RevealedIdleState)
            {
                return UniTask.FromResult(false);
            }

            animFSM.PutCover();

            var putCoverCompletionSource = new UniTaskCompletionSource<bool>();

            void HandlePutCoverComplete()
            {
                putCoverCompletionSource.TrySetResult(true);
                animFSM.PutCoverState.OnComplete -= HandlePutCoverComplete;
            }

            animFSM.PutCoverState.OnComplete -= HandlePutCoverComplete;
            
            return putCoverCompletionSource.Task;
        }
    }
}