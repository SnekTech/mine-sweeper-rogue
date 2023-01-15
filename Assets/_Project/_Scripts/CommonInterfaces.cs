using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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

    public interface ICanAnimate
    {
        Animator Animator { get; }
        SpriteRenderer SpriteRenderer { get; }
    }
}