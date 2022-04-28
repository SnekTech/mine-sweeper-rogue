using System.Collections;
using NSubstitute;
using NUnit.Framework;
using SnekTech.GridCell;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class FlagTests
    {
        [UnityTest]
        public IEnumerator flag_animation_events_should_work()
        {
            FlagBehaviour flagBehaviour = A.FlagBehaviour;
            IFlag flag = flagBehaviour;
            const float secondsToWait = 1;

            bool liftEventInvoked = false, putDownEventInvoked = false;
            flag.LiftCompleted += () =>
            {
                liftEventInvoked = true;
            };
            flag.PutDownCompleted += () =>
            {
                putDownEventInvoked = true;
            };
            flag.Lift();
            yield return new WaitForSeconds(secondsToWait);
            flag.PutDown();
            yield return new WaitForSeconds(secondsToWait);
            
            Assert.IsTrue(liftEventInvoked);
            Assert.IsTrue(putDownEventInvoked);
        }
    }
}
