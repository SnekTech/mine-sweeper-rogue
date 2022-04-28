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
        public IEnumerator emit_PutDown_event_within_1_second_after_calling_PutDown()
        {
            FlagBehaviour flagBehaviour = A.FlagBehaviour.WithActive(true);
            IFlag flag = flagBehaviour;
            const float secondsToWait = 1;

            bool hasCalledHandler = false;
            flag.PutDownCompleted += () =>
            {
                hasCalledHandler = true;
            };
            flag.PutDown();
            yield return new WaitForSeconds(secondsToWait);
            
            Assert.IsTrue(hasCalledHandler);
        }

        [UnityTest]
        public IEnumerator emit_Lift_event_within_1_second_after_calling_Lift()
        {
            FlagBehaviour flagBehaviour = A.FlagBehaviour.WithActive(true);
            IFlag flag = flagBehaviour;
            const float secondsToWait = 1;

            bool eventInvoked = false;
            flag.LiftCompleted += () =>
            {
                eventInvoked = true;
            };
            flag.Lift();
            yield return new WaitForSeconds(secondsToWait);
            
            Assert.IsTrue(eventInvoked);
        }
    }
}
