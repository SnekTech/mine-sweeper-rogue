using UnityEngine;

namespace SnekTech.Grid
{
    [CreateAssetMenu(menuName = C.MenuName.Grid + "/" + nameof(GridData), fileName = nameof(GridData))]
    public class GridData : ScriptableObject
    {
        [SerializeField]
        private GridSize gridSize = new GridSize(15, 15);

        [Range(0, 1)]
        [SerializeField]
        private float bombPercent = 0.2f;

        public GridSize GridSize => gridSize;
        public float BombPercent => bombPercent;
    }
}