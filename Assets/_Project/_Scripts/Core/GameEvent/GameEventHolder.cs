using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekTech.DataPersistence;
using SnekTech.Grid;
using SnekTech.GridCell;
using SnekTech.Player;
using SnekTech.Roguelike;
using SnekTech.UI.Modal;
using UnityEngine;

namespace SnekTech.Core.GameEvent
{
    [CreateAssetMenu]
    public class GameEventHolder : ScriptableObject, IPersistentDataHolder
    {
        [SerializeField]
        private PlayerState playerState;

        [SerializeField]
        private CellEventPool cellEventPool;

        [SerializeField]
        private ModalManager modalManager;

        [SerializeField]
        private GridEventManager gridEventManager;

        private List<CellEvent> _cellEvents;

        public List<CellEvent> CellEvents => _cellEvents;

        // todo: deal with magic number
        private const float CellEventProbability = 1f;
        private readonly IRandomGenerator _cellEventGenerator = RandomGenerator.Instance;

        private const string EventModalHeader = "New Event Triggered";

        #region Unity callbacks

        private void OnEnable()
        {
            gridEventManager.OnCellRecursiveRevealComplete += HandleCellRecursiveRevealComplete;
        }

        private void OnDisable()
        {
            gridEventManager.OnCellRecursiveRevealComplete -= HandleCellRecursiveRevealComplete;
        }

        #endregion

        private async UniTaskVoid HandleCellRecursiveRevealComplete(ICell cell)
        {
            bool shouldTriggerEvent = _cellEventGenerator.NextBool(CellEventProbability);
            if (shouldTriggerEvent)
            {
                CellEventData randomCellEventData = cellEventPool.GetRandom();
                await modalManager.ShowConfirmAsync(EventModalHeader, randomCellEventData.Icon, randomCellEventData.Description);
                
                // todo: set correct levelIndex
                AddCellEvent(new CellEvent(randomCellEventData, cell.GridIndex, 0));
            }
        }

        public void LoadData(GameData gameData)
        {
            List<CellEvent> savedEvents = gameData.cellEvents;
            _cellEvents = new List<CellEvent>(savedEvents);
        }

        public void SaveData(GameData gameData)
        {
            gameData.cellEvents = _cellEvents;
        }

        private void AddCellEvent(CellEvent cellEvent)
        {
            cellEvent.CellEventData.Trigger(playerState);
            _cellEvents.Add(cellEvent);
        }
    }
}