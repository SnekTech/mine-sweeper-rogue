using System;
using SnekTech.Player.OneTimeEffect;
using SnekTech.UI;

namespace SnekTech.Player.ClickEffect
{
    public interface IClickEffect : IOneTimeEffect
    {
        event Action Changed;
        bool IsActive { get; set; }
        IHoverableIconHolder IconHolder { get; set; }
        string Description { get; }
    }
}