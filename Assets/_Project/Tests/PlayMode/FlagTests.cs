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

        private static readonly Flag FlagPrefab = Utils.GetPrefabAsset<Flag>("Flag.prefab");
        
        [UnityTest]
        public IEnumerator call_OnDisappearComplete_after_being_put_down()
        {
            Flag flag = Object.Instantiate(FlagPrefab);
            const float secondsToWait = 1;

            bool hasCalledHandler = false;
            flag.Disappeared += () =>
            {
                hasCalledHandler = true;
            };
            flag.PutDown();
            yield return new WaitForSeconds(secondsToWait);
            
            Assert.IsTrue(hasCalledHandler);
        }
    }
}
