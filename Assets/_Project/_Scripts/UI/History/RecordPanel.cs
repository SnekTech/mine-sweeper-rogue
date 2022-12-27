using SnekTech.Core.History;
using UnityEngine;

namespace SnekTech.UI.History
{
    public class RecordPanel : MonoBehaviour
    {
        [SerializeField]
        private ItemsPanel itemsPanel;
        
        private Record _record;

        public void SetContent(Record record)
        {
            itemsPanel.SetContent(record.Items);
        }
    }
}
