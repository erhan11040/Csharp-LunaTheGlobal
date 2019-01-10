using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LunaTheGlobal.AllCommands
{
    public class SleepPc : PrepareCommand, Command
    {
        public string CommandName = MethodBase.GetCurrentMethod().DeclaringType.Name;

        public SleepPc()
        {

        }


        public string Execute(params string[] parameters)
        {
            try
            {
                Application.SetSuspendState(PowerState.Suspend, true, true);
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }
}
