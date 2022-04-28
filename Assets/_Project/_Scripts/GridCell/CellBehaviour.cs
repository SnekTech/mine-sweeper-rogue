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
        private Cover cover;

        private ICellBrain _cellBrain;

        public IFlag Flag => flagBehaviour;
        public Cover Cover => cover;
        
        private void Awake()
        {
            _cellBrain = new BasicCellBrain(this);
        }

        private void Start()
        {
            Reset();
        }

        private void OnEnable()
        {
            Flag.PutDownCompleted += OnFlagPutDown;
        }

        private void OnDisable()
        {
            Flag.PutDownCompleted -= OnFlagPutDown;
        }

        public void Reset()
        {
            _cellBrain.Reset();
        }

        private void OnFlagPutDown()
        {
            Flag.IsActive = false;
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
