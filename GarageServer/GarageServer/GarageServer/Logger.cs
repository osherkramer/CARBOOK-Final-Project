using System;
using System.IO;

namespace GarageServer
{
    public static class Logger
    {
        private static Config conf;

        static Logger()
        {
            conf = new Config();
            conf.ReadDataFromConfigFile();
        }

        public static void writeMessage(String line)
        {
            StreamWriter file = new StreamWriter(conf.LogFile, true);
            file.WriteLine(DateTime.Now + " [Debug]: " + line);
            file.Close();
        }
        public static void writeError(String line)
        {
            StreamWriter file = new StreamWriter(conf.LogFile, true);
            file.WriteLine(DateTime.Now + " [Attention please - Error]: " + line);
            file.Close();
        }
    }
}
