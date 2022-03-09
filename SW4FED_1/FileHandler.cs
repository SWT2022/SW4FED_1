using SW4FED_1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SW4FED_1
{
    public class FileHandler
    {
        internal static ObservableCollection<Debtors> ReadFile(string fileName)
        {
            // Create an instance of the XmlSerializer class and specify the type of object to deserialize.
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Debtors>));
            TextReader reader = new StreamReader(fileName);
            var debtors = (ObservableCollection<Debtors>)serializer.Deserialize(reader);
            reader.Close();
            return debtors;
        }
        internal static void SaveFile(string fileName, ObservableCollection<Debtors> debtors)
        {
            // Create an instance of the XmlSerializer class and specify the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Debtors>));
            TextWriter writer = new StreamWriter(fileName);
            serializer.Serialize(writer, debtors);
            writer.Close();
        }
    }
}
