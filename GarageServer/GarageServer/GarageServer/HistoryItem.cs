using System;

namespace GarageServer
{
    [Serializable]
    public class HistoryItem
    {
        public string CarNumber { get; set; }
        public CarsItems CareType { get; set; }
        public DateTime Date { get; set; }
        public string[] Treatment { get; set; }
        public int KM { get; set; }
        public string GarageName { get; set; }


        public HistoryItem()
        {
            CarNumber = null;
            Date = DateTime.Now;
        }
    }
}
