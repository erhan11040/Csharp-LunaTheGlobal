using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LunaTheGlobal.AllCommands
{
    class ShutdownLuna : PrepareCommand, Command
    {
        public string CommandName = MethodBase.GetCurrentMethod().DeclaringType.Name;

        public ShutdownLuna()
        {

        }


        public string Execute(params string[] parameters)
        {
            try
            {
                Application.Exit();
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }
}
