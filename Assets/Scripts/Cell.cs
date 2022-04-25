using System;
using UnityEngine;

namespace SnekTech
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Cell : MonoBehaviour
    {
        [SerializeField]
        private CellSprites cellSprites;
        [SerializeField]
        private Flag flag;

        [NonSerialized]
        public CellState CoveredState;
        [NonSerialized]
        public CellState FlaggedState;
        
        private CellState _currentState;


        private void Awake()
        {
            CacheCellStates();
            Debug.Assert(flag != null, "a Cell should have a reference to the child Flag");
            Debug.Assert(!flag.IsActive(), "the Flag under a Cell should be initialized as inactive");
        }

        private void CacheCellStates()
        {
            CoveredState = new CellCoveredState(this);
            FlaggedState = new CellFlaggedState(this);
        }

        private void Start()
        {
            Reset();
        }

        private void OnEnable()
        {
            flag.Disappeared += OnFlagDisappeared;
        }

        private void OnDisable()
        {
            flag.Disappeared -= OnFlagDisappeared;
        }

        public void Reset()
        {
            flag.SetActive(false);
            SwitchState(CoveredState);
        }

        public void SwitchState(CellState state)
        {
            _currentState = state;
            _currentState.OnEnterState();
        }

        public void RaiseFlag()
        {
            if (!flag.IsActive())
            {
                flag.SetActive(true);
            }
        }

        private void OnFlagDisappeared()
        {
            flag.SetActive(false);
        }

        public void PutDownFlag()
        {
            if (flag.IsActive())
            {
                flag.PutDown();
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
