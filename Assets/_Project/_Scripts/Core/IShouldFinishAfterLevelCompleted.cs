using Cysharp.Threading.Tasks;

namespace SnekTech.Core
{
    public interface IShouldFinishAfterLevelCompleted
    {
        UniTask FinishAsync();
    }
}