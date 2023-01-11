using SnekTech.Roguelike;
using UnityEngine;

namespace SnekTech.Grid
{
    [CreateAssetMenu]
    public class GridDataPool : RandomPool<GridData>
    {
        // todo: random this later, use small grid for easier test
        public override GridData GetRandom() => elements[2];
    }
}