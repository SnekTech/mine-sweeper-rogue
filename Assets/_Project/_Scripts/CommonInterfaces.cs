using Cysharp.Threading.Tasks;
using SnekTech.Core.Animation;
using UnityEngine;

namespace SnekTech
{
    public interface ICanClickAsync
    {
        UniTaskVoid ProcessPrimaryAsync(Vector2 mousePosition);
        UniTaskVoid ProcessDoublePrimaryAsync(Vector2 mousePosition);
        UniTaskVoid ProcessSecondaryAsync(Vector2 mousePosition);
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