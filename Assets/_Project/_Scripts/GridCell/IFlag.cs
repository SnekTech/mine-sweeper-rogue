using System;
using Cysharp.Threading.Tasks;

namespace SnekTech.GridCell
{
    public interface IFlag : ICanSwitchActiveness
    {
        event Action LiftCompleted, PutDownCompleted;
        UniTask<bool> LiftAsync();
        UniTask<bool> PutDownAsync();
    }
}