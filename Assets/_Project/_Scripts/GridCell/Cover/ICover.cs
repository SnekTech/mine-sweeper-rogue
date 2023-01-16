using Cysharp.Threading.Tasks;

namespace SnekTech.GridCell.Cover
{
    public interface ICover
    {
        UniTask<bool> RevealAsync();
        UniTask<bool> PutCoverAsync();
    }
}