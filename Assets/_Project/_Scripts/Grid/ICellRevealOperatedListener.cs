using Cysharp.Threading.Tasks;
using SnekTech.GridCell;

namespace SnekTech.Grid
{
    public interface ICellRevealOperatedListener
    {
        UniTask OnCellRevealOperatedAsync(ICell cell);
    }
}