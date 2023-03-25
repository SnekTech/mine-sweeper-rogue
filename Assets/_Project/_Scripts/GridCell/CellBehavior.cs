using SnekTech.MineSweeperRogue.GridSystem;
using SnekTech.MineSweeperRogue.GridSystem.CellSystem;
using UnityEngine;

namespace SnekTech.GridCell
{
    public class CellBehavior : MonoBehaviour, IHumbleCell
    {
        [SerializeField]
        private CellSprites cellSprites;
        
        [SerializeField]
        private SpriteRenderer highlightFrame;

        private SpriteSetter _revealedImageSetter;

        public ICell Cell { get; private set; }
        public IFlag Flag { get; private set; }
        public ICover Cover { get; private set; }
        

        private void Awake()
        {
            _revealedImageSetter = transform.Find("Revealed Image").GetComponent<SpriteSetter>();
            Flag = GetComponentInChildren<IFlag>();
            Cover = GetComponentInChildren<ICover>();
        }

        public void Init(ICell cell)
        {
            Cell = cell;
            SetPosition(cell.Index);
            SetContent(cell.HasBomb ? cellSprites.bombSprite : cellSprites.noBombSprites[cell.NeighborBombCount]);
        }

        private void SetContent(Sprite sprite)
        {
            _revealedImageSetter.Sprite = sprite;
        }

        private void SetPosition(GridIndex gridIndex)
        {
            transform.localPosition = new Vector3(gridIndex.ColumnIndex, gridIndex.RowIndex, 0);
        }

        public void SetHighlight(bool isHighlight)
        {
            highlightFrame.gameObject.SetActive(isHighlight);
        }
    }
}