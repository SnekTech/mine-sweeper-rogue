using SnekTech.GridCell;
using SnekTech.GridCell.Cover;

namespace Tests.PlayMode.Builder
{
    public static class A
    {
        private const string FlagPrefabName = "Flag.prefab";
        private const string CoverPrefabName = "Cover.prefab";
        private const string CellPrefabName = "Cell.prefab";

        public static CoverBehaviour CoverBehaviour => new CoverBehaviourBuilder(CoverPrefabName);
        public static FlagBehaviour FlagBehaviour => new FlagBehaviourBuilder(FlagPrefabName);
        public static CellBehaviour CellBehaviour => new CellBehaviourBuilder(CellPrefabName);

        public static ICover Cover => CoverBehaviour;

        public static IFlag Flag => FlagBehaviour;

        public static ICell Cell => CellBehaviour;
    }

    public static class An
    {
        
    }
}