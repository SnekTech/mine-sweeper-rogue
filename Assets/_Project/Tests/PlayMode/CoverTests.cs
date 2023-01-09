using System.Collections;
using Cysharp.Threading.Tasks;
using Tests.PlayMode.Builder;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class CoverTests : TestBase
    {
        [UnityTest]
        public IEnumerator cell_animation_complete_events_should_emit()
        {
            var cover = A.CoverBehaviour;
            AddToPool(cover);
            
            bool isRevealedCompletedCalled = false, isPutCoverCompletedCalled = false;
            void HandleRevealComplete() => isRevealedCompletedCalled = true;
            void HandlePutCoverComplete() => isPutCoverCompletedCalled = true;
            cover.RevealCompleted += HandleRevealComplete;
            cover.PutCoverCompleted += HandlePutCoverComplete;

            var testRevealTask = Utils.IsConditionMetWhenThrottledTaskCompleteAsync(cover.RevealAsync,
                () => isRevealedCompletedCalled,
                () => cover.RevealCompleted -= HandleRevealComplete);
            var testPutCoverTask = Utils.IsConditionMetWhenThrottledTaskCompleteAsync(cover.PutCoverAsync,
                () => isPutCoverCompletedCalled,
                () => cover.PutCoverCompleted -= HandlePutCoverComplete);

            return Utils.AssertTrueAsync(testRevealTask, testPutCoverTask).ToCoroutine();
        }
    }
}
