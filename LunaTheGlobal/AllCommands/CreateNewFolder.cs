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
    public class CreateNewFolder : PrepareCommand, Command
    {
        public string CommandName = MethodBase.GetCurrentMethod().DeclaringType.Name;
        
        public CreateNewFolder()
        {

        }

        private void CreateFolder(string path, params string[] parameters)
        {
            if (parameters.Count() >=1)
            {
                var i = 0;
                if (parameters[i] == "")
                    i++;
                parameters[i] = parameters[i].Replace(" ", "");
                Directory.CreateDirectory(path + "/" + parameters[i]);

            }
            else
            {
                Directory.CreateDirectory(path + "/NewFolder");

            }
        }

        public string Execute(params string[] parameters)
        {
            try
            {
                if (Maps.Pwd == "")
                {
                    CreateFolder(Maps.Mainpath,parameters);
                }
                else
                {
                    CreateFolder(Maps.Pwd,parameters);
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
