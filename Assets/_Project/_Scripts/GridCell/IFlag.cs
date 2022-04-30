using System;
using System.Threading.Tasks;

namespace SnekTech.GridCell
{
    public interface IFlag
    {
        event Action LiftCompleted, PutDownCompleted;
        void Lift();
        void PutDown();
        bool IsActive { get; set; }
        Task<bool> LiftAsync();
        Task<bool> PutDownAsync();
    }
}