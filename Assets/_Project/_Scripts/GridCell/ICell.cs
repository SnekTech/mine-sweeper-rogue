using System.Threading.Tasks;
using SnekTech.Grid;
using UnityEngine;

namespace SnekTech.GridCell
{
    public interface ICell
    {
        IFlag Flag { get; }
        ICover Cover { get; }
        void Reset();
        void Dispose();

        Task<bool> OnLeftClick();
        Task<bool> OnRightClick();

        bool HasBomb { get; set; }
        void SetContent(Sprite sprite);
        void SetPosition(GridIndex gridIndex);
        bool IsFlagged { get; }
        bool IsCovered { get; }
    }
}