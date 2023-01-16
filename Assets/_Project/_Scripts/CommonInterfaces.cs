using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekTech.Core.Animation.CustomAnimator;
using UnityEngine;

namespace SnekTech
{
    public interface ICanClickAsync
    {
        UniTaskVoid ProcessLeftClickAsync(Vector2 mousePosition);
        UniTaskVoid ProcessLeftDoubleClickAsync(Vector2 mousePosition);
        UniTaskVoid ProcessRightClickAsync(Vector2 mousePosition);
    }

    public interface ICanSwitchActiveness
    {
        bool IsActive { get; set; }
    }

    public interface ICanAnimateSnek
    {
        SnekAnimator SnekAnimator { get; }
        SpriteRenderer SpriteRenderer { get; }
    }
}