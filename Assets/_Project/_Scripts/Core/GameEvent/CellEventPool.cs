using SnekTech.Roguelike;
using UnityEngine;

namespace SnekTech.Core.GameEvent
{
    [CreateAssetMenu]
    public class CellEventPool : RandomPool<CellEventData>
    {
        private const string CellEventDataDir = "/_Project/MyScriptableObjects/CellEvents";
        public override string AssetDirPath => Application.dataPath + CellEventDataDir;
    }
}
