using System;
using SnekTech.GamePlay.OneTimeEffect;
using SnekTech.UI;

namespace SnekTech.GamePlay.ClickEffect
{
    public interface IClickEffect : IOneTimeEffect
    {
        event Action Changed;
        bool IsActive { get; set; }
        IHoverableIconHolder IconHolder { get; set; }
        string Description { get; }
    }
}