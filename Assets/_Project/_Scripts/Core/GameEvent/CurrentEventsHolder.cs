using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekTech.Core.History;
using SnekTech.DataPersistence;
using SnekTech.GamePlay;
using SnekTech.GamePlay.PlayerSystem;
using SnekTech.Grid;
using SnekTech.GridCell;
using SnekTech.Roguelike;
using SnekTech.UI.Modal;
using UnityEngine;

namespace SnekTech.Core.GameEvent
{
    [CreateAssetMenu(menuName = C.MenuName.GameEventSystem + "/" + nameof(CurrentEventsHolder))]
    public class CurrentEventsHolder : ScriptableObject, IPersistentDataHolder
    {
        [SerializeField]
        private Player player;

        [SerializeField]
        private CellEventPool cellEventPool;

        [SerializeField]
        private ModalManager modalManager;

        [SerializeField]
        private GridEventManager gridEventManager;

        [SerializeField]
        private CurrentRecordHolder currentRecordHolder;

        private List<CellEvent> _cellEvents;

        public List<CellEvent> CellEvents => _cellEvents;

        // todo: deal with magic number(for debugging)
        private const float CellEventProbability = 0f;
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

        private void HandleCellRecursiveRevealComplete(ILogicCell cell)
        {
            TryTriggerCellEventAsync(cell).Forget();
        }
        
        private async UniTaskVoid TryTriggerCellEventAsync(ILogicCell cell)
        {
            bool shouldTriggerEvent = _cellEventGenerator.NextBool(CellEventProbability);
            if (shouldTriggerEvent)
            {
                var randomCellEventData = cellEventPool.GetRandom();
                await modalManager.ShowConfirmAsync(EventModalHeader, randomCellEventData.Icon, randomCellEventData.Description);
                
                AddCellEvent(new CellEvent(randomCellEventData, cell.GridIndex, currentRecordHolder.CurrentLevelIndex));
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
            cellEvent.CellEventData.Trigger(player);
            _cellEvents.Add(cellEvent);
        }
    }
}