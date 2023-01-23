namespace SnekTech.GamePlay.EffectSystem
{
    /// <summary>
    /// An effect will be taken on creation, only once.
    /// </summary>
    /// <typeparam name="T">effect target type</typeparam>
    public interface IEffect<in T>
    {
        void Take(T target);
    }
}
