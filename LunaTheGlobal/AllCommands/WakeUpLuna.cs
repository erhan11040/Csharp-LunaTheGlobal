using LunaTheGlobal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LunaTheGlobal.AllCommands
{
    public class WakeUpLuna : PrepareCommand, Command
    {
        public string CommandName = MethodBase.GetCurrentMethod().DeclaringType.Name;

        public WakeUpLuna()
        {

        }


        public string Execute(params string[] parameters)
        {
            try
            {

                Maps.IsSleeping = false;
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }
}
