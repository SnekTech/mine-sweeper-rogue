using System.Collections.Generic;
using SnekTech.GamePlay.CellEventSystem;
using UnityEngine;

namespace SnekTech.UI.History
{
    public class EventsPanel : MonoBehaviour
    {
        [SerializeField]
        private CellEventSlot cellEventSlotPrefab;

        public void SetContent(List<CellEventInstance> cellEvents)
        {
            transform.DestroyAllChildren();
            foreach (var cellEvent in cellEvents)
            {
                var slot = Instantiate(cellEventSlotPrefab, transform);
                slot.SetContent(cellEvent);
            }
        }
    }
}
