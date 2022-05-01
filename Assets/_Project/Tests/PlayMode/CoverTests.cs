using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using SnekTech.GridCell;
using Tests.PlayMode.Builder;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class CoverTests
    {
        private CoverBehaviour _coverBehaviour;
        private ICover _cover;
        
        [SetUp]
        public void SetUp()
        {
            _coverBehaviour = A.CoverBehaviour;
            _cover = _coverBehaviour;
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_coverBehaviour.gameObject);
        }
        
        [UnityTest]
        public IEnumerator cell_animation_complete_events_should_emit()
        {

            bool revealedCompletedCalled = false, putCoverCompletedCalled = false;
            _cover.RevealCompleted += () => revealedCompletedCalled = true;
            _cover.PutCoverCompleted += () => putCoverCompletedCalled = true;

            async Task Run()
            {
                await _cover.RevealAsync();
                await _cover.PutCoverAsync();
                
                Assert.That(revealedCompletedCalled, Is.True);
                Assert.That(putCoverCompletedCalled, Is.True);
            }

            yield return Run().AsCoroutine();
        }
    }
}
