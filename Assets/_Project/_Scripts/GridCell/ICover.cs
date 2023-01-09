using System;
using Cysharp.Threading.Tasks;

namespace SnekTech.GridCell
{
    public interface ICover : ICanSwitchActiveness
    {
        event Action RevealCompleted, PutCoverCompleted;
        UniTask<bool> RevealAsync();
        UniTask<bool> PutCoverAsync();
    }
}