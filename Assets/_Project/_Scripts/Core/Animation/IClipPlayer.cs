using System.Threading;
using Cysharp.Threading.Tasks;

namespace SnekTech.Core.Animation
{
    public interface IClipPlayer
    {
        UniTask PlayAsync(SnekAnimator animator, SnekAnimationClip clip, CancellationToken cancellationToken);
    }
}