using System;
using Cysharp.Threading.Tasks;
using SnekTech.Core.Animation;
using SnekTech.GridCell.Cover.Animation;

namespace SnekTech.GridCell.Cover
{
    public interface ICover : IAnimationContext, ICoverAnimStateProvider
    {
        event Action RevealCompleted, PutCoverCompleted;
        UniTask<bool> RevealAsync();
        UniTask<bool> PutCoverAsync();
    }
}