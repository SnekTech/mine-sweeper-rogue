using SnekTech.Roguelike;
using UnityEngine;

namespace SnekTech.Grid
{
    [CreateAssetMenu(menuName = C.MenuName.Grid + "/" + nameof(GridDataPool))]
    public class GridDataPool : RandomPool<GridData>
    {
        // todo: random this later, use small grid for easier test
        public override GridData GetRandom() => elements[2];
    }
}