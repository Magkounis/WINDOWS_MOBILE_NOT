using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;



namespace SmartDeviceProject1
{
     class connec
    {//create the objects in this class to call them
        public string srv ;
        public string usrnm ;
        public string psw;
        public string db;
        public string macadd;
       

        public void xrdfmxml()//create a new void that will read data from the xml and stores them in the variables above
        {
            XmlDocument doc = new XmlDocument();//create new doc object from class XmlDocument
            doc.Load("C:\\Users\\Administrator\\Documents\\Visual Studio 2010\\Projects\\WindowsFormsApplication1\\WindowsFormsApplication1\\bin\\Debug\\config.xml");//load the xml file
           
            
            XmlNode nodesrv = doc.DocumentElement.SelectSingleNode("/Configuration/Server");//select the appropriate element in the XML file and create an object for that class
            this.srv = nodesrv.InnerText;


            XmlNode nodeusrnm = doc.DocumentElement.SelectSingleNode("/Configuration/Username");
            this.usrnm = nodeusrnm.InnerText;


            XmlNode nodepsw = doc.DocumentElement.SelectSingleNode("/Configuration/Password");
            this.psw = nodepsw.InnerText;


            XmlNode nodedb = doc.DocumentElement.SelectSingleNode("/Configuration/Database");
            this.db = nodedb.InnerText;


            XmlNode nodemc = doc.DocumentElement.SelectSingleNode("/Configuration/Macaddress");
            this.macadd= nodemc.InnerText;






            nodesrv = null;//delete objects to free the memory used
            nodeusrnm = null;
            nodepsw = null;
            doc = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

           
           
           



        }
      


    }


   

}
