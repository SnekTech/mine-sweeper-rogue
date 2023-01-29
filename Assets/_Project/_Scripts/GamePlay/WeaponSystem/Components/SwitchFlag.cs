using Cysharp.Threading.Tasks;
using SnekTech.GridCell;
using UnityEngine;

namespace SnekTech.GamePlay.WeaponSystem.Components
{
    [CreateAssetMenu(menuName = C.MenuName.WeaponComponents + "/" + nameof(SwitchFlag))]
    public class SwitchFlag : WeaponComponent
    {
        public override UniTask Use(ICell cell)
        {
            return cell.SwitchFlag();
        }
    }
}