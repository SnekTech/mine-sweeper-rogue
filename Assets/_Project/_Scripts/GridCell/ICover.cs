using System;
using System.Threading.Tasks;

namespace SnekTech.GridCell
{
    public interface ICover : ICanSwitchActiveness
    {
        event Action RevealCompleted, PutCoverCompleted;
        Task<bool> RevealAsync();
        Task<bool> PutCoverAsync();
    }
}