using System.Collections;
using System.Threading.Tasks;
using UnityEditor;

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
    }
}
