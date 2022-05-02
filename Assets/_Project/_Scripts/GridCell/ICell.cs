using UnityEngine;

namespace SnekTech.GridCell
{
    public interface ICell
    {
        IFlag Flag { get; }
        ICover Cover { get; }
        void Reset();
        void Dispose();

        void OnLeftClick();
        void OnRightClick();

        bool HasBomb { get; set; }
        void SetContent(Sprite sprite);
        void SetPosition(Index2D index);
    }
}