using System.Threading.Tasks;

namespace SnekTech.GridCell
{
    public interface ICover : ICanSwitchActiveness
    {
        Task<bool> RevealAsync();
        Task<bool> PutCoverAsync();
    }
}