using System.Collections;
using NSubstitute;
using NUnit.Framework;
using SnekTech;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class FlagTest
    {
        public const string PrefabsPath = "Assets/_Project/Prefabs";
        
        // A Test behaves as an ordinary method
        [Test]
        public void FlagTestSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator call_OnDisappearComplete_after_being_put_down()
        {
            var flag = AssetDatabase.LoadAssetAtPath<Flag>($"{PrefabsPath}/Flag.prefab");
            const float secondsToWait = 1;
            
            flag.PutDown();
            yield return new WaitForSeconds(secondsToWait);
            
            flag.Received().OnDisappearComplete();
        }
    }
}
