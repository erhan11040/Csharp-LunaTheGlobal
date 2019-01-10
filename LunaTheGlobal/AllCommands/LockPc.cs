using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LunaTheGlobal.AllCommands
{
    public class LockPc : PrepareCommand, Command
    {
        
        public string CommandName = MethodBase.GetCurrentMethod().DeclaringType.Name;
        
        public LockPc()
        {

        }



        public string Execute(params string[] parameters)
        {
            try
            {
                System.Diagnostics.Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }
}
