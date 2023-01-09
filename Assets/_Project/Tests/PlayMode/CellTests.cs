using System.Collections;
using Cysharp.Threading.Tasks;
using SnekTech.GridCell;
using Tests.PlayMode.Builder;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class CellTests : TestBase
    {
        [UnityTest]
        public IEnumerator left_click_a_covered_cell_should_reveal_the_cover()
        {
            var cell = A.CellBehaviour;
            AddToPool(cell);
            
            ICellBrain cellBrain = new BasicCellBrain(cell);
            var cover = cell.Cover;

            bool isCoverRevealCompletedInvoked = false;
            void HandleCoverRevealComplete() => isCoverRevealCompletedInvoked = true;
            cover.RevealCompleted += HandleCoverRevealComplete;

            var testTask = Utils.IsConditionMetWhenThrottledTaskCompleteAsync(
                cellBrain.OnLeftClick,
                () => isCoverRevealCompletedInvoked,
                () => cover.RevealCompleted -= HandleCoverRevealComplete
            );

            return Utils.AssertTrueAsync(testTask).ToCoroutine();
        }

        [UnityTest]
        public IEnumerator right_click_a_covered_cell_should_lift_the_flag()
        {
            var cell = A.CellBehaviour;
            AddToPool(cell);
            
            ICellBrain cellBrain = new BasicCellBrain(cell);
            var flag = cell.Flag;

            bool isLiftFlagCompletedInvoked = false;
            void HandleFlagLiftComplete() => isLiftFlagCompletedInvoked = true;
            flag.LiftCompleted += HandleFlagLiftComplete;

            var testTask = Utils.IsConditionMetWhenThrottledTaskCompleteAsync(
                cellBrain.OnRightClick,
                () => isLiftFlagCompletedInvoked,
                () => flag.LiftCompleted -= HandleFlagLiftComplete
            );

            return Utils.AssertTrueAsync(testTask).ToCoroutine();
        }
 
        [UnityTest]
        public IEnumerator right_click_a_flagged_cell_should_put_down_the_flag()
        {
            var cell = A.CellBehaviour;
            AddToPool(cell);

            ICellBrain cellBrain = new BasicCellBrain(cell);
            var flag = cell.Flag;

            bool isFlagPutDownCompleted = false;
            void HandleFlagPutDownComplete() => isFlagPutDownCompleted = true;
            flag.PutDownCompleted += HandleFlagPutDownComplete;

            async UniTask<bool> TestPutDownFlagTask()
            {
                // make it flagged first
                await cellBrain.OnRightClick();

                // try to put down
                await cellBrain.OnRightClick();
                return true;
            }

            var testTask = Utils.IsConditionMetWhenThrottledTaskCompleteAsync(
                TestPutDownFlagTask,
                () => isFlagPutDownCompleted,
                () => flag.PutDownCompleted -= HandleFlagPutDownComplete
            );

            return Utils.AssertTrueAsync(testTask).ToCoroutine();
        }
    }
}