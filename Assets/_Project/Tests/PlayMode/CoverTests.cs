using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using SnekTech.GridCell;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class CoverTests
    {
        [UnityTest]
        public IEnumerator cell_animation_complete_events_should_emit()
        {
            CoverBehaviour coverBehaviour = A.CoverBehaviour;
            ICover cover = coverBehaviour;

            bool revealedCompletedCalled = false, putCoverCompletedCalled = false;
            cover.RevealCompleted += () => revealedCompletedCalled = true;
            cover.PutCoverCompleted += () => putCoverCompletedCalled = true;

            async Task Run()
            {
                await cover.RevealAsync();
                await cover.PutCoverAsync();
                
                Assert.IsTrue(revealedCompletedCalled);
                Assert.IsTrue(putCoverCompletedCalled);
            }

            yield return Run().AsCoroutine();
        }
    }
}
