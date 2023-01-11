using System.Collections.Generic;
using UnityEngine;

namespace SnekTech.Grid
{
    [CreateAssetMenu]
    public class GridDataPool : ScriptableObject
    {
        [SerializeField]
        private List<GridData> gridDataList;

        // todo: random this
        public GridData GetRandom() => gridDataList[0];

        public void Populate(List<GridData> gridDataListIn) => gridDataList = gridDataListIn;
        public void Clear() => gridDataList.Clear();
    }
}