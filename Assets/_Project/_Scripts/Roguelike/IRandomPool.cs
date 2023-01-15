namespace SnekTech.Roguelike
{
    public interface IRandomPool<T>
    {
        T GetRandom();
    }
}