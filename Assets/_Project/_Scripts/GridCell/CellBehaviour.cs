using System;
using UnityEngine;

namespace SnekTech.GridCell
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CellBehaviour : MonoBehaviour, ICell
    {
        [SerializeField]
        private CellSprites cellSprites;
        [SerializeField]
        private FlagBehaviour flagBehaviour;
        [SerializeField]
        private CoverBehaviour coverBehaviour;

        private ICellBrain _cellBrain;

        public IFlag Flag => flagBehaviour;
        public ICover Cover => coverBehaviour;
        
        private void Awake()
        {
            _cellBrain = new BasicCellBrain(this);
        }

        public void Reset()
        {
            _cellBrain.Reset();
        }

        public void OnLeftClick()
        {
            _cellBrain.OnLeftClick();
        }

        public void OnRightClick()
        {
            _cellBrain.OnRightClick();
        }
    }
}
