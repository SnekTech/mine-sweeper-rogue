using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using SnekTech.GridCell;
using Tests.PlayMode.Builder;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class FlagTests
    {
        private FlagBehaviour _flagBehaviour;
        private IFlag _flag;

        [SetUp]
        public void SetUp()
        {
            _flagBehaviour = A.FlagBehaviour;
            _flag = _flagBehaviour;
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_flagBehaviour.gameObject);
        }

        [UnityTest]
        public IEnumerator flag_animation_complete_events_should_emit()
        {
            bool liftEventInvoked = false, putDownEventInvoked = false;
            _flag.LiftCompleted += () => liftEventInvoked = true;
            _flag.PutDownCompleted += () => putDownEventInvoked = true;

            async Task Run()
            {
                await _flag.LiftAsync();
                await _flag.PutDownAsync();

                Assert.That(liftEventInvoked, Is.True);
                Assert.That(putDownEventInvoked, Is.True);
            }

            yield return Run().AsCoroutine();
        }
    }
}