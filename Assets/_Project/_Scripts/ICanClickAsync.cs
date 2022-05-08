using System.Threading.Tasks;
using UnityEngine;

namespace SnekTech
{
    public interface ICanClickAsync
    {
        Task OnLeftClickAsync(Vector2 mousePosition);
        Task OnRightClickAsync(Vector2 mousePosition);
    }
}