using LunaTheGlobal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LunaTheGlobal.AllCommands
{
    public class SleepLuna : PrepareCommand, Command
    {
        public string CommandName = MethodBase.GetCurrentMethod().DeclaringType.Name;

        public SleepLuna()
        {

        }


        public string Execute(params string[] parameters)
        {
            try
            {
                Maps.IsSleeping = true;
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }
}
