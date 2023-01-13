using System;
using Cysharp.Threading.Tasks;
using SnekTech.GridCell.Cover.Animation;

namespace SnekTech.GridCell.Cover
{
    public interface ICover : ICanAnimate, ICanSwitchActiveness, ICoverAnimStateProvider
    {
        event Action RevealCompleted, PutCoverCompleted;
        UniTask<bool> RevealAsync();
        UniTask<bool> PutCoverAsync();
    }
}