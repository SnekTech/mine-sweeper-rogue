using System.Collections.Generic;
using UnityEngine;

namespace SnekTech.Core.GameEvent
{
    [CreateAssetMenu]
    public class CellEventPool : ScriptableObject
    {
        [SerializeField]
        private List<CellEventData> cellEventsAvailable;

        public CellEventData GetRandom()
        {
            // todo: randomize this behaviour
            // todo: use global random seed
            return cellEventsAvailable[0];
        }
    }
}
