namespace SnekTech.Roguelike
{
    public interface IRandomPool<T> : IPool<T>
    {
        T GetRandom();
    }
}