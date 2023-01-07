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
    public class GameEventHolder : ScriptableObject, IPersistentDataHolder, ICellRevealOperatedListener
    {
        [SerializeField]
        private PlayerState playerState;

        [SerializeField]
        private CellEventPool cellEventPool;

        [SerializeField]
        private ModalManager modalManager;

        private List<CellEvent> _cellEvents;

        public List<CellEvent> CellEvents => _cellEvents;

        // todo: deal with magic number
        // todo: set to zero for debug, remove later
        private readonly IRandomSequence<bool> _cellEventGenerator = new RandomBoolSequence(0, 1f);

        private const string EventModalHeader = "New Event Triggered";

        public async UniTask OnCellRevealOperatedAsync(ICell cell)
        {
            bool shouldTriggerEvent = _cellEventGenerator.Next();
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