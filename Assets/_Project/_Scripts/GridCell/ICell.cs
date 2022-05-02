namespace SnekTech.GridCell
{
    public interface ICell
    {
        void Reset();
        void Dispose();

        void OnLeftClick();
        void OnRightClick();
        
        IFlag Flag { get; }
        ICover Cover { get; }
    }
}