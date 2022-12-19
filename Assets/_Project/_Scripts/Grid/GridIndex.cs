using System;
using UnityEngine;

namespace SnekTech.Grid
{
    [Serializable]
    public class GridIndex
    {
        [SerializeField]
        private int rowIndex;
        [SerializeField]
        private int columnIndex;

        public int RowIndex
        {
            get => rowIndex;
            set => rowIndex = value;
        }

        public int ColumnIndex
        {
            get => columnIndex;
            set => columnIndex = value;
        }

        public static GridIndex Zero = new GridIndex(0, 0);

        public GridIndex(int rowIndex, int columnIndex)
        {
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
        }

        public GridIndex(GridIndex other): this(other.RowIndex, other.ColumnIndex)
        {
        }

        public static GridIndex operator +(GridIndex a, GridIndex b)
        {
            return new GridIndex(a.RowIndex + b.RowIndex, a.ColumnIndex + b.ColumnIndex);
        }

        public static implicit operator GridIndex(Vector2Int vector2Int)
        {
            return new GridIndex(vector2Int.y, vector2Int.x);
        }
    }
}