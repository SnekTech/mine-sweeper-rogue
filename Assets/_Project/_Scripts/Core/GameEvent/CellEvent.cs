using System;
using SnekTech.Player;
using UnityEngine;

namespace SnekTech.Core.GameEvent
{
    [Serializable]
    public class CellEvent
    {
        // todo: more cell event meta data
        [SerializeField]
        private string emittedLevel = "default level";

        [SerializeField]
        private CellEventData cellEventData;
        
        public CellEvent(CellEventData cellEventData)
        {
            this.cellEventData = cellEventData;
        }

        public void Emit(PlayerState playerState)
        {
            cellEventData.Emit(playerState);
        }

        // todo: make use of cell event meta data
        public string EmittedLevel => emittedLevel;
    }
}
