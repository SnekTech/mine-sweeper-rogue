using System.Collections.Generic;
using SnekTech.Core.GameEvent;
using UnityEngine;

namespace SnekTech.UI.History
{
    public class EventsPanel : MonoBehaviour
    {
        [SerializeField]
        private CellEventSlot cellEventSlotPrefab;

        public void SetContent(List<CellEvent> cellEvents)
        {
            transform.DestroyAllChildren();
            foreach (CellEvent cellEvent in cellEvents)
            {
                CellEventSlot slot = Instantiate(cellEventSlotPrefab, transform);
                slot.SetContent(cellEvent);
            }
        }
    }
}
