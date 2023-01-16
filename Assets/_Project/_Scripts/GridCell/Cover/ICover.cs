using Cysharp.Threading.Tasks;

namespace SnekTech.GridCell.Cover
{
    public interface ICover : ILogicCover, ICanAnimate
    {
    }

    public interface ILogicCover
    {
        UniTask<bool> RevealAsync();
        UniTask<bool> PutCoverAsync();
    }
}