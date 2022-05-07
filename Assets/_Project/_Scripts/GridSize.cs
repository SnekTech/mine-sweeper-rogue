﻿using System;

namespace SnekTech
{
    [Serializable]
    public struct GridSize
    {
        public int rowCount;
        public int columnCount;

        public GridSize(int rowCount, int columnCount)
        {
            this.rowCount = rowCount;
            this.columnCount = columnCount;
        }
    }
}