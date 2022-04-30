using System.Threading.Tasks;

namespace SnekTech.GridCell
{
    public interface ICover : ICanSwitchActiveness
    {
        Task<bool> OpenAsync();
        Task<bool> CloseAsync();
    }
}