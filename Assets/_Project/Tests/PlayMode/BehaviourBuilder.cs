using UnityEngine;

namespace Tests.PlayMode
{
    public abstract class BehaviourBuilder<T> where T : MonoBehaviour
    {
        private readonly T _behaviourPrefab;

        protected BehaviourBuilder(string prefabName)
        {
            _behaviourPrefab = Utils.GetPrefabAsset<T>(prefabName);
        }

        private T Build()
        {
            return Object.Instantiate(_behaviourPrefab);
        }

        public static implicit operator T(BehaviourBuilder<T> builder)
        {
            return builder.Build();
        }
    }
}
