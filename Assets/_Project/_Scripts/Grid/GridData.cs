using UnityEngine;

namespace SnekTech.Grid
{
    [CreateAssetMenu(fileName = nameof(GridData))]
    public class GridData : ScriptableObject
    {
        public GridSize GridSize = new GridSize(15, 15);
        [Range(0, 1)]
        public float BombPercent = 0.2f;
        [Min(0)]
        public int BombGeneratorSeed;
    }
}