namespace SnekTech.GamePlay.EffectSystem
{
    public interface IEffect<in T>
    {
        void Take(T target);
    }
}
