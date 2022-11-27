using System;
using SnekTech.Grid;
using UnityEngine;

namespace SnekTech.Core
{
    public class Judge : MonoBehaviour
    {
        [SerializeField]
        private GridEventManager gridEventManager;
        
        private IGameMode _gameMode;
        private bool _hasGivenJudgement;

        private void Awake()
        {
            _gameMode = new ClassicGameMode(gridEventManager);
        }

        private void OnEnable()
        {
            gridEventManager.GridInitCompleted += OnGridInitCompleted;
        }

        private void OnDisable()
        {
            gridEventManager.GridInitCompleted -= OnGridInitCompleted;
        }

        private void Update()
        {
            if (_hasGivenJudgement)
            {
                return;
            }
            
            if (_gameMode.HasLevelCleared)
            {
                // todo: load next level &| congratulate
                _hasGivenJudgement = true;
            }
        }

        private void OnGridInitCompleted(IGrid obj)
        {
            Init();
        }
        
        private void Init()
        {
            _hasGivenJudgement = false;
        }
        
    }
}
