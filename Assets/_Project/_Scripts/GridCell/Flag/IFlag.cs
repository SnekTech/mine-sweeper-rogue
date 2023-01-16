using System;
using Cysharp.Threading.Tasks;

namespace SnekTech.GridCell.Flag
{
    public interface IFlag : ICanSwitchActiveness, ICanAnimate
    {
        UniTask<bool> LiftAsync();
        UniTask<bool> PutDownAsync();
    }
}