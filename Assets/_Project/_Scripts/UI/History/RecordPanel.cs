using SnekTech.Core.History;
using UnityEngine;

namespace SnekTech.UI.History
{
    public class RecordPanel : MonoBehaviour
    {
        [SerializeField]
        private EventsPanel eventsPanel;
        [SerializeField]
        private ItemsPanel itemsPanel;
        
        private Record _record;

        public void SetContent(Record record)
        {
            eventsPanel.SetContent(record.CellEvents);
            itemsPanel.SetContent(record.Items);
        }
    }
}
