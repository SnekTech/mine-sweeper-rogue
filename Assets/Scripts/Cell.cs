using System;
using UnityEngine;

namespace SnekTech
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Cell : MonoBehaviour
    {
        [SerializeField]
        private CellSprites cellSprites;

        [NonSerialized]
        public CellState CoveredState;
        [NonSerialized]
        public CellState FlaggedState;
        
        private CellState _currentState;

        private Flag _flag;

        private void Awake()
        {
            CacheCellStates();
        }

        private void CacheCellStates()
        {
            CoveredState = new CellCoveredState(this);
            FlaggedState = new CellFlaggedState(this);
        }

        private void Start()
        {
            _flag = GetComponentInChildren<Flag>();
            _flag.Disappeared += OnFlagDisappeared;

            Reset();
        }

        public void Reset()
        {
            _flag.SetActive(false);
            SwitchState(CoveredState);
        }

        public void SwitchState(CellState state)
        {
            _currentState = state;
            _currentState.OnEnterState();
        }

        public void RaiseFlag()
        {
            if (!_flag.IsActive())
            {
                _flag.SetActive(true);
            }
        }

        private void OnFlagDisappeared()
        {
            _flag.SetActive(false);
        }

        public void PutDownFlag()
        {
            if (_flag.IsActive())
            {
                _flag.PutDown();
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
    }
}
