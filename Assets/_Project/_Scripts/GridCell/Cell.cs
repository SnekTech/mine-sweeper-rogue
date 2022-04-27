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
        private FlagBehaviour flagBehaviour;
        [SerializeField]
        private Cover cover;

        [NonSerialized]
        public CellState CoveredState;
        [NonSerialized]
        public CellState FlaggedState;
        [NonSerialized]
        public CellState RevealedState;
        
        private CellState _currentState;

        public FlagBehaviour FlagBehaviour => flagBehaviour;

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
            FlagBehaviour.Disappeared += OnFlagDisappeared;
        }

        private void OnDisable()
        {
            FlagBehaviour.Disappeared -= OnFlagDisappeared;
        }

        public void Reset()
        {
            FlagBehaviour.SetActive(false);
            SwitchState(CoveredState);
        }

        public void SwitchState(CellState state)
        {
            _currentState = state;
            _currentState.OnEnterState();
        }

        public void RaiseFlag()
        {
            if (!FlagBehaviour.IsActive())
            {
                FlagBehaviour.SetActive(true);
            }
        }

        private void OnFlagDisappeared()
        {
            FlagBehaviour.SetActive(false);
        }

        public void PutDownFlag()
        {
            if (FlagBehaviour.IsActive())
            {
                FlagBehaviour.PutDown();
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
