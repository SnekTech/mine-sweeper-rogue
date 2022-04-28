using SnekTech.GridCell;
using UnityEditor;
using UnityEngine;

namespace Tests.PlayMode
{
    public class FlagBehaviourBuilder
    {
        private const string PrefabName = "Flag.prefab";
        private static readonly FlagBehaviour FlagBehaviourPrefab = Utils.GetPrefabAsset<FlagBehaviour>(PrefabName);

        private FlagBehaviour Build()
        {
            FlagBehaviour flag = Object.Instantiate(FlagBehaviourPrefab);

            return flag;
        }

        public static implicit operator FlagBehaviour(FlagBehaviourBuilder builder)
        {
            return builder.Build();
        }
    }
}
