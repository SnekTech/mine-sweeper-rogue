using System;
using Cysharp.Threading.Tasks;

namespace SnekTech.GridCell.Flag
{
    public interface IFlag : ILogicFlag, ICanAnimate
    {
    }

    public interface ILogicFlag
    {
        UniTask<bool> LiftAsync();
        UniTask<bool> PutDownAsync();
    }
}