﻿namespace Tests.PlayMode.Builder
{
    public static class A
    {
        public static FlagBehaviourBuilder FlagBehaviour => new FlagBehaviourBuilder("Flag.prefab");
        public static CoverBehaviourBuilder CoverBehaviour => new CoverBehaviourBuilder("Cover.prefab");
        public static CellBehaviourBuilder CellBehaviour => new CellBehaviourBuilder("Cell.prefab");
    }

    public static class An
    {
        
    }
}
