using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using SnekTech.GridCell;
using Tests.PlayMode.Builder;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class CellTests
    {
        private CellBehaviour _cellBehaviour;
        private ICell _cell;
        
        [SetUp]
        public void SetUp()
        {
            _cellBehaviour = A.CellBehaviour;
            _cell = _cellBehaviour;
        }
        
        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_cellBehaviour.gameObject);
        }
        
        [UnityTest]
        public IEnumerator left_click_after_cell_brain_init_will_reveal_the_cover()
        {
            ICellBrain cellBrain = new BasicCellBrain(_cell);

            bool isCoverRevealCompletedInvoked = false;
            cellBrain.Cover.RevealCompleted += () => isCoverRevealCompletedInvoked = true;

            async Task Run()
            {
                await Utils.UntilComplete(cellBrain.OnLeftClick);

                Assert.That(isCoverRevealCompletedInvoked, Is.True);
            }

            yield return Run().AsCoroutine();
        }

        [UnityTest]
        public IEnumerator right_click_after_cell_init_should_lift_the_flag()
        {
            ICellBrain cellBrain = new BasicCellBrain(_cell);

            bool isLiftFlagCompletedInvoked = false;
            cellBrain.Flag.LiftCompleted += () => isLiftFlagCompletedInvoked = true;

            async Task Run()
            {
                await Utils.UntilComplete(cellBrain.OnRightClick);

                Assert.That(isLiftFlagCompletedInvoked, Is.True);
            }

            yield return Run().AsCoroutine();
        }
    }
}