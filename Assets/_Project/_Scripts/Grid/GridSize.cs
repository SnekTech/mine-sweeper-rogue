using System;
using UnityEngine;

namespace SnekTech.Grid
{
    [Serializable]
    public class GridSize
    {
        [Range(0, 50)]
        public int rowCount;
        [Range(0, 50)]
        public int columnCount;

        public GridSize(int rowCount, int columnCount)
        {
            this.rowCount = rowCount;
            this.columnCount = columnCount;
        }
    }
}