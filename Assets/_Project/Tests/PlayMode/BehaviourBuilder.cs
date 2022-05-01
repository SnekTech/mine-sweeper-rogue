using UnityEngine;

namespace Tests.PlayMode
{
    public abstract class BehaviourBuilder<T> where T : MonoBehaviour
    {
        protected readonly T BehaviourPrefab;

        protected BehaviourBuilder(string prefabName)
        {
            BehaviourPrefab = Utils.GetPrefabAsset<T>(prefabName);
        }

        protected abstract T Build();

        public static implicit operator T(BehaviourBuilder<T> builder)
        {
            return builder.Build();
        }
    }
}
