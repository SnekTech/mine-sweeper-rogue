using System.Collections;
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
        public IEnumerator call_OnDisappearComplete_after_being_put_down()
        {
            FlagBehaviour flagBehaviour = Object.Instantiate(FlagBehaviourPrefab);
            const float secondsToWait = 1;

            bool hasCalledHandler = false;
            flagBehaviour.Disappeared += () =>
            {
                hasCalledHandler = true;
            };
            flagBehaviour.PutDown();
            yield return new WaitForSeconds(secondsToWait);
            
            Assert.IsTrue(hasCalledHandler);
        }
    }
}
