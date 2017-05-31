using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace GarageServer
{
    public static class XMLHelper
    {
        public static bool WriteToFile<T>(T serializeObject, string fileName)
        {
            if (serializeObject == null)
            {
                return false;
            }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer xmlSerializer = new XmlSerializer(serializeObject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    xmlSerializer.Serialize(stream, serializeObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
            }
            catch (Exception e)
            {
                Logger.writeError(e.ToString());
                return false;
            }

            return true;
        }

        public static T ReadFromFile<T>(string fileName)
        {
            if(string.IsNullOrEmpty(fileName))
            {
                return default(T);
            }

            T objectOut = default(T);

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type typeOut = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(typeOut);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch(Exception e)
            {
                Logger.writeError(e.ToString());
                return default(T);
            }

            return objectOut;
        }
    }
}
