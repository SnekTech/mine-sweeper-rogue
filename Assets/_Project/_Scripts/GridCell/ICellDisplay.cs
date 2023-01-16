using SnekTech.Grid;
using UnityEngine;

namespace SnekTech.GridCell
{
    public interface ICellDisplay
    {
        void SetContent(Sprite sprite);
        void SetPosition(GridIndex gridIndex);
        void SetHighlight(bool isHighlight);
    }
}