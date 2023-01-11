using SnekTech.Roguelike;
using UnityEngine;

namespace SnekTech.Grid
{
    [CreateAssetMenu]
    public class GridDataPool : RandomPool<GridData>
    {
        private const string GridDataDir = "/_Project/MyScriptableObjects/Static";
        public override string AssetDirPath => Application.dataPath + GridDataDir;

        // todo: random this
        public override GridData GetRandom() => elements[0];
    }
}