using Cysharp.Threading.Tasks;
using SnekTech.Grid;
using SnekTech.GridCell.Cover;
using SnekTech.GridCell.Flag;
using SnekTech.GridCell.FSM;
using UnityEngine;

namespace SnekTech.GridCell
{
    public class Cell : MonoBehaviour, ICell
    {
        [SerializeField]
        private SpriteRenderer highlightFrame;

        private CellFSM _fsm;

        private SpriteSetter _revealedImageSetter;

        public bool HasBomb { get; set; }

        public ILogicFlag Flag { get; private set; }
        public ILogicCover Cover { get; private set; }
        public bool IsFlagged => _fsm.IsFlagged;
        public bool IsCovered => _fsm.IsCovered;
        public bool IsRevealed => _fsm.IsRevealed;

        public GridIndex GridIndex { get; set; }


        private void Awake()
        {
            _revealedImageSetter = transform.Find("Revealed Image").GetComponent<SpriteSetter>();
            Flag = GetComponentInChildren<IFlag>();
            Cover = GetComponentInChildren<ICover>();

            _fsm = new CellFSM(this);
        }

        public UniTask<bool> OnPrimary() => _fsm.OnPrimary();

        public UniTask<bool> OnSecondary() => _fsm.OnSecondary();

        public void SetContent(Sprite sprite)
        {
            _revealedImageSetter.Sprite = sprite;
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