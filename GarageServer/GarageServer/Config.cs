using System;

namespace GarageServer
{
    [Serializable]
    public class Config
    {
        public string SavedDataLocation { get; set; }
        public string TreatmentsLocation { get; set; }
        public string CarsData { get; set; }
        public string LogFile { get; set; }
        public string ZippedData { get; set; }

        public void ReadDataFromConfigFile()
        {
            Config config = XMLHelper.ReadFromFile<Config>(@"C:\Users\osher\Desktop\CarBook\ConfigFileGarageServer.xml");
            this.SavedDataLocation = config.SavedDataLocation;
            this.TreatmentsLocation = config.TreatmentsLocation;
            this.CarsData = config.CarsData;
            this.LogFile = config.LogFile;
            this.ZippedData = config.ZippedData;
        }
    }
}
