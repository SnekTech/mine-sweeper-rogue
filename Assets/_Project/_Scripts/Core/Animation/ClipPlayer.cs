using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SnekTech.Core.Animation
{
    public abstract class ClipPlayer : ScriptableObject, IClipPlayer
    {
        public abstract UniTask PlayAsync(SnekAnimator animator, SnekAnimationClip clip, CancellationToken cancellationToken);
    }
}