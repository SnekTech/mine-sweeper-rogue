using System;
using System.Threading.Tasks;

namespace SnekTech.GridCell
{
    public interface IFlag : ICanSwitchActiveness
    {
        event Action LiftCompleted, PutDownCompleted;
        Task<bool> LiftAsync();
        Task<bool> PutDownAsync();
    }
}