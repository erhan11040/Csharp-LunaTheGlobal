using LunaTheGlobal.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LunaTheGlobal.AllCommands
{
    public class CreateNewFile : PrepareCommand, Command
    {
        public string CommandName = MethodBase.GetCurrentMethod().DeclaringType.Name;
        private string path="";
        public CreateNewFile()
        {

        }
        
        private void CreateFile(string path,params string[] parameters)
        {
            if (parameters.Count() >=1)
            {
                var i = 0;
                if (parameters[i] == "")
                    i++;
                if(parameters[i]!="")
                {
                    parameters[i] = parameters[i].Replace(" dot ", ".");
                    parameters[i] = parameters[i].Replace(" dot", ".");
                    parameters[i] = parameters[i].Replace(" ", "");
                    StreamWriter Dosya = File.CreateText(path + "/" + parameters[i]);
                    this.path = path + "/" + parameters[i];
                    Dosya.Close();
                }
                else
                {
                    StreamWriter Dosya = File.CreateText(path + "/NewFile.txt");
                    this.path = path + "/NewFile.txt";
                    Dosya.Close();
                }
            }
            else
            {
                StreamWriter Dosya = File.CreateText(path + "/NewFile.txt");
                this.path = path + "/NewFile.txt";
                Dosya.Close();
            }

        }
        public string Execute(params string[] parameters)
        {
            try
            {
                
                if(Maps.Pwd=="")
                {
                    CreateFile(Maps.Mainpath,parameters);
                }
                else
                {
                    CreateFile(Maps.Pwd,parameters);
                }
                
                
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
        public string ExecuteReturnPath(params string[] parameters)
        {
            try
            {

                if (Maps.Pwd == "")
                {
                    CreateFile(Maps.Mainpath);
                }
                else
                {
                    CreateFile(Maps.Pwd);
                }


                return this.path ;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

    }
}
