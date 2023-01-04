using UnityEngine;

namespace SnekTech.UI
{
    public interface IHoverableIconHolder
    {
        Sprite Icon { get; }
        string Label { get; }
        string Description { get; }
    }
}