using LunaTheGlobal.Common;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LunaTheGlobal.AllCommands
{
    public class SearchInPc : PrepareCommand, Command
    {
        public string CommandName = MethodBase.GetCurrentMethod().DeclaringType.Name;
        public SearchInPc()
        {
            
        }

       
        public string Execute(params string[] parameters)
        {
            try
            {
                int counter = 0;
                if (parameters[counter] == "")
                    counter++;

               Search search = new Search();
               var items= search.SearchFile(parameters[counter],"File", "file:E:/");
                //search.DirSearch("E", "Steam.exe");
                string result="";
                int a = 0;
                foreach(var i in items )
                {
                    a++;
                    result += a+":"+i+"\n";
                    
                }
                var items2 = search.SearchFile(parameters[counter], "File", "file:C:/");
                a = 0;
                foreach (var i in items)
                {
                    a++;
                    result += a + ":" + i + "\n";

                }
                return result;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }
}
