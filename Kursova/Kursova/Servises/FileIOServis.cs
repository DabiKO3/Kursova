using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Kursova;

namespace Kursova.Servises
{
    internal class FileIOServis
    {
        private readonly string PATH;
        public FileIOServis(string path)
        {
            PATH = path;
        }
        public List<Enterprise> LoadDate()
        {
            var fileExist = File.Exists(PATH);
            if (!fileExist)
            {
                File.CreateText(PATH).Dispose();
                return new List<Enterprise>();
            }
            using (var reader = File.OpenText(PATH))
            {
                //var fileText = reader.ReadToEnd();
                //return JsonConvert.DeserializeObject<BindingList<Enterprise>>(fileText);
                return new List<Enterprise>();
            }
        }
        public void SaveDateXML(object dateList)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Enterprise>));

            using (FileStream fs = new FileStream(PATH, FileMode.Create))
            {
                serializer.Serialize(fs, dateList);
            }

        }

        public void SaveDateBinary(object dateList)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(PATH, FileMode.Create))
            {
                formatter.Serialize(fs, dateList);
            }

        }
    }
}
