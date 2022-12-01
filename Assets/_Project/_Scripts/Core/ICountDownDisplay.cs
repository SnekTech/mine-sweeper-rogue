namespace SnekTech.Core
{
    public interface ICountDownDisplay
    {
        void UpdateDurationRemaining(float durationRemaining);
        void SetActive(bool isActive);
    }
}