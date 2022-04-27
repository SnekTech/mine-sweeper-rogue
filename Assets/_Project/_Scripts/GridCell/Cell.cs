using System;
using UnityEngine;

namespace SnekTech.GridCell
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Cell : MonoBehaviour
    {
        [SerializeField]
        private CellSprites cellSprites;
        [SerializeField]
        private Flag flag;
        [SerializeField]
        private Cover cover;

        [NonSerialized]
        public CellState CoveredState;
        [NonSerialized]
        public CellState FlaggedState;
        [NonSerialized]
        public CellState RevealedState;
        
        private CellState _currentState;

        public Flag Flag => flag;

        private void Awake()
        {
            CacheCellStates();
        }

        private void CacheCellStates()
        {
            CoveredState ??= new CellCoveredState(this);
            FlaggedState ??= new CellFlaggedState(this);
            RevealedState ??= new CellRevealedState(this);
        }

        private void Start()
        {
            Reset();
        }

        private void OnEnable()
        {
            Flag.Disappeared += OnFlagDisappeared;
        }

        private void OnDisable()
        {
            Flag.Disappeared -= OnFlagDisappeared;
        }

        public void Reset()
        {
            Flag.SetActive(false);
            SwitchState(CoveredState);
        }

        public void SwitchState(CellState state)
        {
            _currentState = state;
            _currentState.OnEnterState();
        }

        public void RaiseFlag()
        {
            if (!Flag.IsActive())
            {
                Flag.SetActive(true);
            }
        }

        private void OnFlagDisappeared()
        {
            Flag.SetActive(false);
        }

        public void PutDownFlag()
        {
            if (Flag.IsActive())
            {
                Flag.PutDown();
            }
        }

        public void OnLeftClick()
        {
            _currentState.OnLeftClick();
        }

        public void OnRightClick()
        {
            _currentState.OnRightLick();
        }

        public void RevealCover()
        {
            cover.Reveal();
        }
    }
}
