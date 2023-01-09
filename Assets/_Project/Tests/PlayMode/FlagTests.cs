using System.Collections;
using Cysharp.Threading.Tasks;
using Tests.PlayMode.Builder;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class FlagTests : TestBase
    {
        [UnityTest]
        public IEnumerator flag_animation_complete_events_should_emit()
        {
            var flag = A.FlagBehaviour;
            AddToPool(flag);
            
            bool isLiftCompleteInvoked = false, isPutDownCompletedInvoked = false;
            void HandleLiftComplete() => isLiftCompleteInvoked = true;
            void HandlePutDownComplete() => isPutDownCompletedInvoked = true;
            flag.LiftCompleted += HandleLiftComplete;
            flag.PutDownCompleted += HandlePutDownComplete;

            var testLiftTask = Utils.IsConditionMetWhenThrottledTaskCompleteAsync(flag.LiftAsync,
                () => isLiftCompleteInvoked,
                () => flag.LiftCompleted -= HandleLiftComplete);
            var testPutDownTask = Utils.IsConditionMetWhenThrottledTaskCompleteAsync(flag.PutDownAsync,
                () => isPutDownCompletedInvoked,
                () => flag.PutDownCompleted -= HandlePutDownComplete);

            return Utils.AssertTrueAsync(testLiftTask, testPutDownTask).ToCoroutine();
        }
    }
}