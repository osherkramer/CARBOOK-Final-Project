using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectV1.Models
{
    public class HistoryCar
    {
        //Car
        public Car car { get; set; }

        //History Item
        public List<HistoryItem> historyItems { get; set; }

        public bool isOwnerRequest { get; set; }

        public bool isPublished { get; set; }
    }
}