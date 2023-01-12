using Cysharp.Threading.Tasks;
using SnekTech.Grid;
using SnekTech.GridCell.Cover;
using UnityEngine;

namespace SnekTech.GridCell
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CellBehaviour : MonoBehaviour, ICell
    {
        [SerializeField]
        private SpriteRenderer highlightFrame;

        private ICellBrain _cellBrain;
        private SpriteRenderer _spriteRenderer;
        
        public bool HasBomb { get; set; }
        
        public IFlag Flag { get; private set; }
        public ICover Cover { get; private set; }
        public bool IsFlagged => _cellBrain.IsFlagged;
        public bool IsCovered => _cellBrain.IsCovered;
        public bool IsRevealed => _cellBrain.IsRevealed;
        
        public GridIndex GridIndex { get; set; }


        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Flag = GetComponentInChildren<IFlag>();
            Cover = GetComponentInChildren<ICover>();
            
            _cellBrain = new BasicCellBrain(this);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        public UniTask<bool> OnLeftClick()
        {
            return _cellBrain.OnLeftClick();
        }

        public UniTask<bool> OnRightClick()
        {
            return _cellBrain.OnRightClick();
        }

        public void SetContent(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public void SetPosition(GridIndex gridIndex)
        {
            transform.localPosition = new Vector3(gridIndex.ColumnIndex, gridIndex.RowIndex, 0);
        }

        public void SetHighlight(bool isHighlight)
        {
            highlightFrame.gameObject.SetActive(isHighlight);
        }
    }
}