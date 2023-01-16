using UnityEngine;

namespace SnekTech.Core.GameModeSystem
{
    [CreateAssetMenu(menuName = C.MenuName.GameModeSystem + "/" + nameof(GameModeInfo))]
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
