using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using SnekTech.GridCell;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class FlagTests
    {
        [UnityTest]
        public IEnumerator flag_animation_complete_events_should_emit()
        {
            FlagBehaviour flagBehaviour = A.FlagBehaviour;
            IFlag flag = flagBehaviour;

            bool liftEventInvoked = false, putDownEventInvoked = false;
            flag.LiftCompleted += () => liftEventInvoked = true;
            flag.PutDownCompleted += () => putDownEventInvoked = true;
            
            yield return Run().AsCoroutine();
            async Task Run()
            {
                await flag.LiftAsync();
                await flag.PutDownAsync();
                
                Assert.IsTrue(liftEventInvoked);
                Assert.IsTrue(putDownEventInvoked);
            }
        }
    }
}
