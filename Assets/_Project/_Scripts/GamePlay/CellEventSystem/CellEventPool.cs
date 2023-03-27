using SnekTech.Roguelike;
using UnityEngine;

namespace SnekTech.GamePlay.CellEventSystem
{
    [CreateAssetMenu(menuName = C.MenuName.GameEventSystem + "/" + nameof(CellEventPool))]
    public class CellEventPool : RandomPool<CellEvent>
    {
    }
}
