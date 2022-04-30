using System;
using System.Threading.Tasks;

namespace SnekTech.GridCell
{
    public interface IFlag : ICanSwitchActiveness
    {
        event Action LiftCompleted, PutDownCompleted;
        void Lift();
        void PutDown();
        Task<bool> LiftAsync();
        Task<bool> PutDownAsync();
    }
}