using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekTech.C;
using SnekTech.Core.History;
using SnekTech.DataPersistence;
using SnekTech.GamePlay.PlayerSystem;
using SnekTech.GridSystem;
using SnekTech.MineSweeperRogue.GridSystem.CellSystem;
using SnekTech.Roguelike;
using SnekTech.UI.Modal;
using UnityEngine;

namespace SnekTech.GamePlay.CellEventSystem
{
    [CreateAssetMenu(menuName = MenuName.GameEventSystem + "/" + nameof(CurrentEventsHolder))]
    public class CurrentEventsHolder : ScriptableObject, IPersistentDataHolder
    {
        [SerializeField]
        private Player player;

        [SerializeField]
        private CellEventPool cellEventPool;

        [SerializeField]
        private ModalManager modalManager;

        [SerializeField]
        private GridEventChannel gridEventChannel;

        [SerializeField]
        private CurrentRecordHolder currentRecordHolder;

        private List<CellEventInstance> _cellEvents;

        public List<CellEventInstance> CellEvents => _cellEvents;

        // todo: deal with magic number(for debugging)
        private const float CellEventProbability = 1f;
        private readonly IRandomGenerator _cellEventGenerator = RandomGenerator.Instance;

        private const string EventModalHeader = "New Event Triggered";

        #region Unity callbacks

        private void OnEnable()
        {
            gridEventChannel.OnCellRecursiveRevealComplete += HandleCellRecursiveRevealComplete;
        }
        
        private void OnDisable()
        {
            gridEventChannel.OnCellRecursiveRevealComplete -= HandleCellRecursiveRevealComplete;
        }

        #endregion

        private void HandleCellRecursiveRevealComplete(ICell cell)
        {
            TryTriggerCellEventAsync(cell).Forget();
        }

        private async UniTaskVoid TryTriggerCellEventAsync(ICell cell)
        {
            var shouldTriggerEvent = _cellEventGenerator.NextBool(CellEventProbability);
            if (shouldTriggerEvent)
            {
                var randomCellEventData = cellEventPool.GetRandom();
                
                // todo: use a notification instead
                // await modalManager.ShowConfirmAsync(EventModalHeader, randomCellEventData.Icon,
                //     randomCellEventData.Description);

                await AddCellEvent(new CellEventInstance(randomCellEventData, cell.Index, currentRecordHolder.CurrentLevelIndex));
            }
        }

        public void LoadData(GameData gameData)
        {
            var savedEvents = gameData.cellEvents;
            _cellEvents = new List<CellEventInstance>(savedEvents);
        }

        public void SaveData(GameData gameData)
        {
            gameData.cellEvents = _cellEvents;
        }

        private async UniTask AddCellEvent(CellEventInstance cellEventInstance)
        {
            await cellEventInstance.CellEvent.Trigger(player);
            _cellEvents.Add(cellEventInstance);
        }
    }
}