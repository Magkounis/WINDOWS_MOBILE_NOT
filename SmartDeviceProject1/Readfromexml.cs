using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartDeviceProject1
{
   public class Readfromexml
    {
        public string mode { get; set; }
        public string serverip { get; set; }
        public string formenabled { get; set; }
        public string run { get; set; }
        public string error { get; set; }
        public Readfromexml(string namexml)
        {
            try
            {
                string path;
                path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);//get the path of the exe
                XmlDocument doc = new XmlDocument();
                doc.Load(path+"\\"+namexml);
                path = null;
                XmlNode node = doc.DocumentElement.SelectSingleNode("serverip");
                serverip = node.InnerText;
                XmlNode node2 = doc.DocumentElement.SelectSingleNode("formenabled");
                formenabled = node2.InnerText;
                XmlNode node3 = doc.DocumentElement.SelectSingleNode("mode");
                mode = node3.InnerText;
                doc = null;
                node = null;
            }
            catch (Exception exp)
            {
                
                error="ERROR IN Reading from config xml:" + exp.ToString();

            }
           
            

        }



    }
}
