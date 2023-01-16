using System.Collections.Generic;
using UnityEngine;

namespace SnekTech
{
    [CreateAssetMenu(menuName = C.MenuName.Grid + "/" + nameof(CellSprites), fileName = nameof(CellSprites))]
    public class CellSprites : ScriptableObject
    {
        public List<Sprite> noBombSprites = new List<Sprite>();

        public Sprite bombSprite;
    }
}
