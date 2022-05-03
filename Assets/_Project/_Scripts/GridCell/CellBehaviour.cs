using System;
using System.Threading.Tasks;
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
        public bool IsFlagged => _cellBrain.IsFlagged;
        public bool IsCovered => _cellBrain.IsCovered;
        
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

        public Task<bool> OnLeftClick()
        {
            return _cellBrain.OnLeftClick();
        }

        public Task<bool> OnRightClick()
        {
            return _cellBrain.OnRightClick();
        }

        public void SetContent(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public void SetPosition(Index2D index)
        {
            transform.localPosition = new Vector3(index.ColumnIndex, index.RowIndex, 0);
        }
    }
}
