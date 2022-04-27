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

        private static readonly FlagBehaviour FlagBehaviourPrefab = Utils.GetPrefabAsset<FlagBehaviour>("Flag.prefab");
        
        [UnityTest]
        public IEnumerator emit_PutDown_event_within_1_second_after_calling_PutDown()
        {
            IFlag flag = Object.Instantiate(FlagBehaviourPrefab);
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
            IFlag flag = Object.Instantiate(FlagBehaviourPrefab);
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
