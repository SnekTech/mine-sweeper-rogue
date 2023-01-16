using Cysharp.Threading.Tasks;
using SnekTech.Grid;
using SnekTech.GridCell;
using SnekTech.GridCell.Cover;
using SnekTech.GridCell.Flag;

namespace Tests.EditMode.Builder
{
    public class FakeCell : ILogicCell
    {
        public GridIndex GridIndex { get; set; } = GridIndex.Zero;
        public IFlag Flag { get; } = new FakeFlag();
        public ICover Cover { get; } = new FakeCover();

        public UniTask<bool> OnPrimary() => UniTask.FromResult(true);
        public UniTask<bool> OnSecondary() => UniTask.FromResult(true);

        public bool HasBomb { get; set; } = false;

        public bool IsFlagged => false;
        public bool IsCovered => true;
        public bool IsRevealed => false;
    }
}