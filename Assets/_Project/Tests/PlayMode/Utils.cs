﻿using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Tests.PlayMode
{
    public static class Utils
    {
        public const string PrefabsPath = "Assets/_Project/Prefabs";

        public static T GetPrefabAsset<T> (string prefabFilename) where T : UnityEngine.Object 
            => AssetDatabase.LoadAssetAtPath<T>($"{PrefabsPath}/{prefabFilename}");

        public static IEnumerator AsCoroutine(this Task task)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }
            task.GetAwaiter().GetResult(); 
        }

        public static async Task<bool> AttemptUntilSuccess(Func<Task<bool>> taskProvider, int attemptIntervalMilliseconds = 20, int maxAttemptCount = 100)
        {
            int i = 0;
            while (!await taskProvider())
            {
                if (i >= maxAttemptCount)
                {
                    Debug.LogWarning($"{nameof(AttemptUntilSuccess)} exceeds max run count");
                    return false;
                }

                await Task.Delay(attemptIntervalMilliseconds);
                i++;
            }

            return true;
        }
    }
}
