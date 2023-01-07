namespace SnekTech.Roguelike
{
    public interface IRandomGenerator
    {
        void Init(string seed);
        string Seed { get; }
        int GenerationCount { get; }
        int Next();
        int Next(int maxValue);
        int Next(int minValue, int maxValue);
        bool NextBool(float probability);
    }
}
