using System.Collections.Generic;
using UnityEngine;

namespace SnekTech
{
    [CreateAssetMenu(menuName = "Cell Spites")]
    public class CellSprites : ScriptableObject
    {
        public List<Sprite> noBombSprites = new List<Sprite>();

        public Sprite bombSprite;
    }
}
