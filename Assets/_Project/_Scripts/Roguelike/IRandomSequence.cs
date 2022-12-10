namespace SnekTech.Roguelike
{
    public interface IRandomSequence<out T>
    {
        void SetSeed(int seed);
        T Next();
    }
}