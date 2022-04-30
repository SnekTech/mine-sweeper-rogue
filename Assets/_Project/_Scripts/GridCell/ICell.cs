namespace SnekTech.GridCell
{
    public interface ICell
    {
        void Reset();

        void OnLeftClick();
        void OnRightClick();
        
        IFlag Flag { get; }
        ICover Cover { get; }
    }
}