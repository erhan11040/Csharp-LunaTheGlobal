using LunaTheGlobal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LunaTheGlobal.AllCommands
{
    public class DefineProjectName : PrepareCommand, Command
    {
        public string CommandName = MethodBase.GetCurrentMethod().DeclaringType.Name;

        public DefineProjectName()
        {

        }


        public string Execute(params string[] parameters)
        {
            try
            {
                if (parameters.Count() >= 1)
                {
                    Maps.CurrentProjectName = parameters[0];
                    return "Success";
                }
                else
                {
                    return "Please give a parameter";
                }
                
                
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }
}
