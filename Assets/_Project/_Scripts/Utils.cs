using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SnekTech
{
    public static class Utils
    {
        public static Vector3 GetMouseWorldPosition(Vector2 screenPosition, Camera camera)
        {
            return camera.ScreenToWorldPoint(screenPosition);
        }

        public static Vector3 GetMouseWorldPosition()
        {
            Vector2 screenPosition = Mouse.current.position.ReadValue();
            Camera mainCamera = Camera.main;
            return GetMouseWorldPosition(screenPosition, mainCamera);
        }
    }

    [Serializable]
    public struct GridSize
    {
        public int rowCount;
        public int columnCount;

        public GridSize(int rowCount, int columnCount)
        {
            this.rowCount = rowCount;
            this.columnCount = columnCount;
        }
    }

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
