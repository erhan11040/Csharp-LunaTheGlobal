using LunaTheGlobal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LunaTheGlobal.AllCommands
{
    public class GoTo : PrepareCommand, Command
    {
        public string CommandName = MethodBase.GetCurrentMethod().DeclaringType.Name;
        public GoTo()
        {

        }
        public string Execute(params string[] parameters)
        {
            try
            {
                if (parameters.Count() >= 1)
                {
                    var i = 0;
                    if (parameters[i] == "")
                        i++;
                    
                    parameters[i] = parameters[i].Replace(" ", "");

                    if(parameters[i].Length<2)
                    {
                        Maps.Pwd = parameters[i] + ":";
                    }
                    else
                    {
                        if(parameters[i]=="see")
                            Maps.Pwd = "C" + ":";
                        else
                            Maps.Pwd += "/"+parameters[i];
                    }
                   
                }
                else
                {
                    return "Please give me a parameter to go!";
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
