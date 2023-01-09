using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Tests.PlayMode
{
    public static class Utils
    {
        private const string PrefabsPath = "Assets/_Project/Prefabs";

        private const string TaskFailedMessage = "task not successful";

        public static T GetPrefabAsset<T> (string prefabFilename) where T : UnityEngine.Object 
            => AssetDatabase.LoadAssetAtPath<T>($"{PrefabsPath}/{prefabFilename}");

        public static async UniTask<bool> CanSucceedBeforeTimeout(Func<UniTask<bool>> throttledTaskProvider,
            int attemptIntervalMilliseconds = 20, int maxAttemptCount = 100)
        {
            int i = 0;
            while (!await throttledTaskProvider())
            {
                if (i >= maxAttemptCount)
                {
                    Debug.LogWarning($"{nameof(CanSucceedBeforeTimeout)} exceeds max run count");
                    return false;
                }

                await UniTask.Delay(attemptIntervalMilliseconds);
                i++;
            }

            return true;
        }

        public static async UniTask<bool> CanPassBeforeTimeout(Func<bool> predicate, int durationSeconds = 10)
        {
            int resultIndex = await UniTask.WhenAny(UniTask.WaitUntil(predicate), UniTask.Delay(TimeSpan.FromSeconds(durationSeconds)));
            return resultIndex == 0;
        }
        
        public static async UniTask AssertTrueAsync(params UniTask<bool>[] predicateTasks)
        {
            bool[] results = await UniTask.WhenAll(predicateTasks);
            Assert.True(results.All(result => result));
        }

        public static async UniTask<bool> IsConditionMetWhenThrottledTaskCompleteAsync(
            Func<UniTask<bool>> throttledTaskProvider,
            Func<bool> predicate,
            Action onCleanUp = null)
        {
            bool isTaskSuccessful = await CanSucceedBeforeTimeout(throttledTaskProvider);
            onCleanUp?.Invoke();
            
            if (!isTaskSuccessful)
            {
                throw new Exception(TaskFailedMessage);
            }

            return predicate();
        }
    }
}
