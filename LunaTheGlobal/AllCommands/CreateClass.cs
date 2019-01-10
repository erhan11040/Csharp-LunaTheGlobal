using LunaTheGlobal.Common;
using LunaTheGlobal.Languages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LunaTheGlobal.AllCommands
{
    public class CreateClass : PrepareCommand, Command
    {
        public string CommandName = MethodBase.GetCurrentMethod().DeclaringType.Name;
        public CreateClass()
        {

        }
        private string PascalCase(string ClassName)
        {
            string Result="";
            string[] Pieces=ClassName.Split(' ');
            foreach(var item in Pieces)
            {
                if(item!="")
                    Result += item.First().ToString().ToUpper() + item.Substring(1);
            }
            Result = Result.Replace(" ", "");
            return "";
        }
        public string Execute(params string[] parameters)
        {
            try
            {
                if (parameters != null)
                {
                    var i = 0;
                    if (parameters[i] == "")
                        i++;

                    CreateNewFile CNF = new CreateNewFile();
                    string ClassName = "";
                    if (parameters[i + 1] != null && parameters[i + 1] != "")
                        ClassName= PascalCase(parameters[i+1]);

                    switch(Maps.CurrentLanguage.ToLower())
                    {
                        case "c#":
                            
                            string text = File.ReadAllText(CSharp.LibraryFilepath + "/Class.txt");

                            
                            text = text.Replace("ClassName", ClassName);

                            CNF.ExecuteReturnPath(Maps.Pwd+ClassName+".cs");
                            
                                

                            text = text.Replace("AccessLevel", parameters[i]);
                            text = text.Replace("ProjectName", Maps.CurrentProjectName);
                            text = text.Replace("Folder", "");
                            
                            File.WriteAllText(Maps.Pwd+"", text);
                            break;
                        case "php":
                            break;
                    }

                   

                }
                else
                {
                    return "Please give me a parameter!";
                }

                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }
}
