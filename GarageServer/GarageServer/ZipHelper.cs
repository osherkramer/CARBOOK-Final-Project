using System;
using System.IO.Compression;

namespace GarageServer
{
    public static class ZipHelper
    {
        public static bool Compress(string FileToCompress, string PathToStoreCompareFile)
        {
            try
            {
                ZipFile.CreateFromDirectory(FileToCompress, PathToStoreCompareFile);
            }
            catch(Exception e)
            {
                Logger.writeError(e.ToString());
                return false;
            }
            return true;
        }

        public static bool Extract(string FileToExtract, string PathToStore)
        {
            try
            {
                ZipFile.ExtractToDirectory(FileToExtract, PathToStore);
            }
            catch (Exception e)
            {
                Logger.writeError(e.ToString());
                return false;
            }
            return true;
        }
    }
}
