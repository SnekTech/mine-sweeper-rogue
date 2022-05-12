using System.Threading.Tasks;
using UnityEngine;

namespace SnekTech
{
    public interface ICanClickAsync
    {
        void OnLeftClickAsync(Vector2 mousePosition);
        void OnLeftDoubleClickAsync(Vector2 mousePosition);
        void OnRightClickAsync(Vector2 mousePosition);
    }
}