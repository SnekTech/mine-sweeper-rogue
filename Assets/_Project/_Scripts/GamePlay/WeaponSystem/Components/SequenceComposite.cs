using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekTech.GridCell;
using UnityEngine;

namespace SnekTech.GamePlay.WeaponSystem.Components
{
    [CreateAssetMenu(menuName = C.MenuName.WeaponComponents + "/" + nameof(SequenceComposite))]
    public class SequenceComposite : WeaponComponent
    {
        [SerializeReference]
        private List<WeaponComponent> components;

        public override async UniTask Use(ICell targetCell)
        {
            foreach (var component in components)
            {
                await component.Use(targetCell);
            }
        }
    }
}
