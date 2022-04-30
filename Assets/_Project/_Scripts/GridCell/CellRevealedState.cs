﻿using System.Threading.Tasks;

namespace SnekTech.GridCell
{
    public class CellRevealedState : CellState
    {
        public CellRevealedState(ICellBrain cellBrain) : base(cellBrain)
        {
        }

        public override void OnEnterState()
        {
            CellBrain.Cover.OpenAsync();
        }

        public override void OnLeaveState()
        {
            
        }

        public override void OnLeftClick()
        {
        }

        public override void OnRightLick()
        {
        }
    }
}
