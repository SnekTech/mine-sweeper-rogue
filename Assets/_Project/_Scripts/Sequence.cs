namespace SnekTech
{
    public interface ISequence<out T>
    {
        void Reset(); 
        T Next();
    }
}