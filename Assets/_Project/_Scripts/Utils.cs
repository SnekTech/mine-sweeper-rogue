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

        public static Task<T> GetCompletedTask<T>(T result)
        {
            var tcs = new TaskCompletionSource<T>();
            tcs.SetResult(result);
            return tcs.Task;
        }
    }

    public struct Index2D
    {
        public int RowIndex;
        public int ColumnIndex;

        public static Index2D Zero = new Index2D(0, 0);

        public Index2D(int rowIndex, int columnIndex)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
        }

        public static Index2D operator +(Index2D a, Index2D b)
        {
            return new Index2D(a.RowIndex + b.RowIndex, a.ColumnIndex + b.ColumnIndex);
        }

        public static implicit operator Index2D(Vector2Int vector2Int)
        {
            return new Index2D(vector2Int.y, vector2Int.x);
        }
    }
}
