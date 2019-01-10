using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LunaTheGlobal.Common
{
    public class ReadFile
    {
        public ReadFile()
        {
            
            

        }
        public void CommandNameToAlias(string CommandName )
        {

           

            //string text = System.IO.File.ReadAllText(map.Mainpath+"/");


            string[] lines = System.IO.File.ReadAllLines(Maps.CommandConfigPath);
            //XmlDocument doc = new XmlDocument();
            //doc.Load("c:\\temp.xml");
            /*XDocument doc = XDocument.Load("myfile.xml");
            var addresses = from address in doc.Root.Elements("address")
                            where address.Element("firstName").Value.Contains("er")
                            select address;*/

            // Loading from a file, you can also load from a stream
            var xml = XDocument.Load(Maps.CommandConfigPath);


            // Query the data and write out a subset of contacts
            var query = from c in xml.Root.Descendants("contact")
                        where (int)c.Attribute("id") < 4
                        select c.Element("firstName").Value + " " +
                               c.Element("lastName").Value;


            foreach (string name in query)
            {
                Console.WriteLine("Contact's Full Name: {0}", name);
            }
            foreach (string line in lines)
            {


            }

        }
    }
}
