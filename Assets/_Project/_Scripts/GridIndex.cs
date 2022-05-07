using UnityEngine;

namespace SnekTech
{
    public struct GridIndex
    {
        public readonly int RowIndex;
        public readonly int ColumnIndex;

        public static GridIndex Zero = new GridIndex(0, 0);

        public GridIndex(int rowIndex, int columnIndex)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
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