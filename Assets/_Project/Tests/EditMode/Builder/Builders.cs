using SnekTech.GridCell;
using SnekTech.GridCell.Cover;
using SnekTech.GridCell.Flag;

namespace Tests.EditMode.Builder
{
    public static class A
    {
        public static ILogicCell Cell => new FakeCell();
        public static ILogicCover Cover => new FakeCover();
        public static ILogicFlag Flag => new FakeFlag();
    }
}
