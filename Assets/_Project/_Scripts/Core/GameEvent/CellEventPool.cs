using SnekTech.Roguelike;
using UnityEngine;

namespace SnekTech.Core.GameEvent
{
    [CreateAssetMenu(menuName = C.MenuName.GameEventSystem + "/" + nameof(CellEventPool))]
    public class CellEventPool : RandomPool<CellEventData>
    {
    }
}
