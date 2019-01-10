using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LunaTheGlobal.AllCommands
{
    class Shutdown : PrepareCommand, Command
    {
        public string CommandName = MethodBase.GetCurrentMethod().DeclaringType.Name;
        public Shutdown()
        {

        }
        public string Execute(params string[] parameters)
        {
            try
            {
                var psi = new ProcessStartInfo("shutdown", "/s /t 0");
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                Process.Start(psi);
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }
}
