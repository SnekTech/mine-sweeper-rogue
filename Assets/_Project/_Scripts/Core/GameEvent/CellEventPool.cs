using System.Collections.Generic;
using SnekTech.Roguelike;
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
            return cellEventsAvailable.GetRandom();
        }
    }
}
