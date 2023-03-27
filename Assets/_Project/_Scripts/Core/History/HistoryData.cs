using System;
using System.Collections.Generic;

namespace SnekTech.Core.History
{
    [Serializable]
    public class HistoryData
    {
        public List<Record> records;

        public HistoryData()
        {
            records = new List<Record>();
        }
    }
}
