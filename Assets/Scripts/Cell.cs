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
        private Flag flagPrefab;

        [NonSerialized]
        public CellState CoveredState;
        [NonSerialized]
        public CellState FlaggedState;
        
        private CellState _currentState;

        private SpriteRenderer _spriteRenderer;
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
            _spriteRenderer = GetComponent<SpriteRenderer>();

            Reset();
        }

        public void Reset()
        {
            
            SwitchState(CoveredState);
        }

        public void SwitchState(CellState state)
        {
            _currentState = state;
            _currentState.OnEnterState();
        }

        public void RaiseFlag()
        {
            if (_flag == null)
            {
                _flag = Instantiate(flagPrefab, transform);
                _flag.DisappearCompleted += OnFlagDisappearCompleted;
            }
            else
            {
                if (!_flag.gameObject.activeInHierarchy)
                {
                    _flag.gameObject.SetActive(true);
                }
            }
        }

        private void OnFlagDisappearCompleted()
        {
            _flag.gameObject.SetActive(false);
        }

        public void PutDownFlag()
        {
            if (_flag == null)
            {
                return;
            }

            if (_flag.gameObject.activeInHierarchy)
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
