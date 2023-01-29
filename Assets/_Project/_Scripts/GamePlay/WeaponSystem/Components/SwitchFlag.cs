using Cysharp.Threading.Tasks;
using SnekTech.GridCell;
using UnityEngine;

namespace SnekTech.GamePlay.WeaponSystem.Components
{
    [CreateAssetMenu(menuName = C.MenuName.WeaponComponents + "/" + nameof(SwitchFlag))]
    public class SwitchFlag : WeaponComponent
    {
        public override async UniTask Use(ICell cell)
        {
            bool isClickSuccessful = await cell.SwitchFlag();

            // todo: invoke below
            // if (isClickSuccessful)
            // {
            //     gridEventChannel.InvokeOnCellFlagOperated(this, cell);
            // }
        }
    }
}