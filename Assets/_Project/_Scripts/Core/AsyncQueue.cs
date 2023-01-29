using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SnekTech.Core
{
    [CreateAssetMenu(menuName = C.MenuName.UIManagers + "/" + nameof(AsyncQueue))]
    public class AsyncQueue : ScriptableObject
    {
        public readonly List<UniTask> tasks = new List<UniTask>();

        public void Add(UniTask task) => tasks.Add(task);
    }
}