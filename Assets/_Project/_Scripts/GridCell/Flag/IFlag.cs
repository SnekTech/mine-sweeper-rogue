using System;
using Cysharp.Threading.Tasks;

namespace SnekTech.GridCell.Flag
{
    public interface IFlag : ICanSwitchActiveness, ICanAnimate
    {
        event Action LiftCompleted, PutDownCompleted;
        UniTask<bool> LiftAsync();
        UniTask<bool> PutDownAsync();
    }
}