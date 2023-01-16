using Cysharp.Threading.Tasks;

namespace SnekTech.GridCell.Flag
{
    public interface IFlag : ILogicFlag, ICanAnimateSnek
    {
    }

    public interface ILogicFlag
    {
        UniTask<bool> LiftAsync();
        UniTask<bool> PutDownAsync();
    }
}