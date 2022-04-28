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

        public CellState CoveredState { get; private set; }
        public CellState FlaggedState { get; private set; }
        public CellState RevealedState { get; private set; }
        
        private CellState _currentState;

        public IFlag Flag => flagBehaviour;

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
            Flag.PutDownCompleted += OnFlagPutDown;
        }

        private void OnDisable()
        {
            Flag.PutDownCompleted -= OnFlagPutDown;
        }

        public void Reset()
        {
            Flag.Hide();
            SwitchState(CoveredState);
        }

        public void SwitchState(CellState state)
        {
            _currentState = state;
            _currentState.OnEnterState();
        }

        public void LiftFlag()
        {
            if (Flag.IsActive)
            {
                throw new ApplicationException("Raising an active flag is invalid.");
            }
            Flag.Show();
            Flag.Lift();
        }

        private void OnFlagPutDown()
        {
            Flag.Hide();
        }

        public void PutDownFlag()
        {
            if (Flag.IsActive)
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
