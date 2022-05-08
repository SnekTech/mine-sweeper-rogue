namespace SnekTech.Grid
{
    public record GridData
    {
        public GridSize GridSize;
        public float BombPercent;
        public int BombGeneratorSeed;

        public static GridData Default => new GridData
        {
            BombGeneratorSeed = 0,
            BombPercent = 0.1f,
            GridSize = new GridSize(10, 10),
        };
    }
}