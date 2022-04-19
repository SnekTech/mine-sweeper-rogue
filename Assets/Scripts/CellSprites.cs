using System.Collections.Generic;
using UnityEngine;

namespace SnekTech
{
    [CreateAssetMenu(menuName = "Cell Spites")]
    public class CellSprites : ScriptableObject
    {
        [SerializeField]
        private List<Sprite> sprites = new List<Sprite>();

        public Sprite Get(CellStatus cellStatus)
        {
            int index = (int) cellStatus;
            Debug.Assert(index >= 0 && index < sprites.Count);
            return sprites[(int) cellStatus];
        }
    }
}
