using System;
using Cysharp.Threading.Tasks;

namespace SnekTech.GridCell.Cover
{
    public interface ICover : ICanAnimate, ICanSwitchActiveness
    {
        event Action RevealCompleted, PutCoverCompleted;
        UniTask<bool> RevealAsync();
        UniTask<bool> PutCoverAsync();
    }
}