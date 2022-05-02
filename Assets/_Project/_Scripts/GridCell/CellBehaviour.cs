using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SnekTech.GridCell
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CellBehaviour : MonoBehaviour, ICell
    {
        [SerializeField]
        private FlagBehaviour flagBehaviour;
        [SerializeField]
        private CoverBehaviour coverBehaviour;

        private ICellBrain _cellBrain;

        private SpriteRenderer _spriteRenderer;
        
        public bool HasBomb { get; set; }
        
        public IFlag Flag => flagBehaviour;
        public ICover Cover => coverBehaviour;
        
        private void Awake()
        {
            _cellBrain = new BasicCellBrain(this);

            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Reset()
        {
            _cellBrain.Reset();
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        public void OnLeftClick()
        {
            _cellBrain.OnLeftClick();
        }

        public void OnRightClick()
        {
            _cellBrain.OnRightClick();
        }

        public void SetContent(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }
    }
}
