using System.Collections.Generic;
using SnekTech.Grid;
using SnekTech.GridCell;
using SnekTech.Player;
using SnekTech.Roguelike;
using UnityEngine;

namespace SnekTech.Core.GameEvent
{
    [CreateAssetMenu]
    public class GameEventHolder : ScriptableObject
    {
        [SerializeField]
        private PlayerState playerState;

        [SerializeField]
        private GridEventManager gridEventManager;

        [SerializeField]
        private CellEventPool cellEventPool;
        
        private List<CellEvent> _cellEvents;

        public List<CellEvent> CellEvents => _cellEvents;

        // todo: deal with magic number
        // todo: set to zero for debug, remove later
        private readonly IRandomSequence<bool> _cellEventGenerator = new RandomBoolSequence(0, 1f);

        private void OnEnable()
        {
            gridEventManager.CellRevealOperated += OnCellRevealOperated;
        }

        private void OnDisable()
        {
            gridEventManager.CellRevealOperated -= OnCellRevealOperated;
        }

        private void OnCellRevealOperated(ICell cell)
        {
            bool shouldTriggerEvent = _cellEventGenerator.Next();
            if (shouldTriggerEvent)
            {
                CellEventData randomCellEventData = cellEventPool.GetRandom();
                
                AddCellEvent(new CellEvent(randomCellEventData, cell.GridIndex, 0));
            }
        }

        public void Load(PlayerData playerData)
        {
            List<CellEvent> savedEvents = playerData.cellEvents;
            _cellEvents = new List<CellEvent>();

            foreach (CellEvent savedEvent in savedEvents)
            {
                AddCellEvent(savedEvent);
            }
        }

        private void AddCellEvent(CellEvent cellEvent)
        {
            if (cellEvent.IsActive)
            {
                cellEvent.CellEventData.Trigger(playerState);
            }
            _cellEvents.Add(cellEvent);
        }
    }
}