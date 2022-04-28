namespace SnekTech.GridCell
{
    public interface ICell
    {
        void Reset();

        void OnLeftClick();
        void OnRightClick();
        
        IFlag Flag { get; }
        Cover Cover { get; }
    }
}