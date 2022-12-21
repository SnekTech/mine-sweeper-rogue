using UnityEngine;

namespace SnekTech.Core.GameModeSystem
{
    [CreateAssetMenu]
    public class GameModeInfo : ScriptableObject
    {
        [SerializeField]
        private string gameModeName;

        [SerializeField]
        private string description;

        public string GameModeName => gameModeName;
        public string Description => description;
    }
}
