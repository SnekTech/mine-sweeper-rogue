using UnityEngine;
using UnityEngine.UI;

namespace SnekTech.UI
{
    public class FlexibleGridLayout : LayoutGroup
    {
        public enum FitType
        {
            Uniform,
            Width,
            Height,
            FixedRows,
            FixedColumns,
        }
        
        public FitType fitType = FitType.Uniform;
        
        [Min(1)]
        public int rows;

        [Min(1)]
        public int columns;
        
        public Vector2 cellSize;
        public Vector2 spacing;

        public bool fitX;
        public bool fitY;
        
        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

            if (fitType is FitType.Width or FitType.Height or FitType.Uniform)
            {
                fitX = true;
                fitY = true;
                float sqrRt = Mathf.Sqrt(transform.childCount);
                rows = Mathf.CeilToInt(sqrRt);
                columns = Mathf.CeilToInt(sqrRt);
            }

            if (fitType is FitType.Width or FitType.FixedColumns)
            {
                rows = Mathf.CeilToInt(transform.childCount / (float) columns);
            }

            if (fitType is FitType.Height or FitType.FixedRows)
            {
                columns = Mathf.CeilToInt(transform.childCount / (float) rows);
            }

            Rect parentRect = rectTransform.rect;
            float parentWidth = parentRect.width;
            float parentHeight = parentRect.height;

            float cellWidth = parentWidth / columns - spacing.x / columns * (columns - 1)
                                                    - padding.left / (float) columns - padding.right / (float) columns;
            float cellHeight = parentHeight / rows - spacing.y / rows * (rows - 1)
                                                   - padding.top / (float) rows - padding.bottom / (float) rows;

            cellSize.x = fitX ? cellWidth : cellSize.x;
            cellSize.y = fitY ? cellHeight : cellSize.y;

            for (int i = 0; i < rectChildren.Count; i++)
            {
                int rowIndex = i / columns;
                int columnIndex = i % columns;

                RectTransform item = rectChildren[i];

                float xPos = cellSize.x * columnIndex + spacing.x * columnIndex + padding.left;
                float yPos = cellSize.y * rowIndex + spacing.y * rowIndex + padding.top;
                
                SetChildAlongAxis(item, 0, xPos, cellSize.x);
                SetChildAlongAxis(item, 1, yPos, cellSize.y);
            }
        }

        public override void CalculateLayoutInputVertical()
        {
        }

        public override void SetLayoutHorizontal()
        {
        }

        public override void SetLayoutVertical()
        {
        }
    }
}