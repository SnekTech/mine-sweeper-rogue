using SnekTech.GridCell;
using UnityEditor;
using UnityEngine;

namespace Tests.PlayMode
{
    public class FlagBehaviourBuilder
    {
        private const string PrefabName = "Flag.prefab";
        private static readonly FlagBehaviour FlagBehaviourPrefab = Utils.GetPrefabAsset<FlagBehaviour>(PrefabName);

        private bool _isActive = true;

        public FlagBehaviourBuilder WithIsActive(bool isActive)
        {
            _isActive = isActive;
            return this;
        }

        private FlagBehaviour Build()
        {
            FlagBehaviour flag = Object.Instantiate(FlagBehaviourPrefab);
            flag.gameObject.SetActive(_isActive);

            return flag;
        }

        public static implicit operator FlagBehaviour(FlagBehaviourBuilder builder)
        {
            return builder.Build();
        }
    }
}
