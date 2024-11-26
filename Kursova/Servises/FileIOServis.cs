using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
                
                return new List<Enterprise>();
            }
        }
        public void SaveDateXML(object dateList)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BindingList<Enterprise>));

            using (FileStream fs = new FileStream(PATH, FileMode.Create))
            {
                serializer.Serialize(fs, dateList);
            }

        }

        public (bool, Exception) SaveDateTXT(BindingList<Enterprise> dateList)
        {
            StreamWriter sw;
            sw = new StreamWriter(PATH, false, Encoding.UTF8);
            try
            {
                foreach (Enterprise enterprise in dateList)
                {
                    sw.Write(enterprise.Name + "\t" + enterprise.Poslygu + "\t" +
                        enterprise.FormaVlasnosty + "\t" + enterprise.Specialization + "\t" +
                        enterprise.Phone + "\t" + enterprise.IsSayt.ToString() + "\t" +
                        enterprise.Address + "\t" + enterprise.DaysWork +
                        "\t" + enterprise.TimeWork + "\t" + enterprise.QualitSrvices + "\t" + enterprise.Rozryad + "\t\n");
                }
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
            finally
            {
                sw.Close();
            }
            return (true, null);   

        }

        public BindingList<Enterprise> LoadFromTXT()
        {
            Enterprises enterprises = new Enterprises();
            StreamReader sr;
            sr = new StreamReader(PATH, Encoding.UTF8);
            string s;
            try
            {
                while ((s = sr.ReadLine()) != null)
                {
                    string[] split = s.Split('\t');
                    Enterprise enterprise = new Enterprise(split[0], int.Parse(split[10]), split[6], split[4], split[3], split[8], split[7], split[1], split[2], bool.Parse(split[5]), double.Parse(split[9]));

                    enterprises.AddEnterprise(enterprise);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталась помилка: \n{0}", ex.Message,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sr.Close();
            }

            return enterprises.Data;
        }


        public BindingList<Enterprise> LoadFromXML()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BindingList<Enterprise>));

            using (FileStream fs = new FileStream(PATH, FileMode.Open))
            {
                return (BindingList<Enterprise>)serializer.Deserialize(fs);
            }
        }

    }
}
