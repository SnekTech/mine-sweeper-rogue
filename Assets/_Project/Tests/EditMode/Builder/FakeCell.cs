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
        public IGrid ParentGrid { get; set; }
        public ILogicFlag Flag { get; } = new FakeFlag();
        public ILogicCover Cover { get; } = new FakeCover();

        public UniTask<bool> Reveal() => UniTask.FromResult(true);
        public UniTask<bool> SwitchFlag() => UniTask.FromResult(true);

        public bool HasBomb { get; set; } = false;

        public bool IsFlagged => false;
        public bool IsCovered => true;
        public bool IsRevealed => false;
    }
}