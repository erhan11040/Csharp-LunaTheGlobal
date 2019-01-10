using LunaTheGlobal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;

namespace LunaTheGlobal.AllCommands
{
    public class UnlockPc : PrepareCommand, Command
    {
        public string CommandName = MethodBase.GetCurrentMethod().DeclaringType.Name;

        public UnlockPc()
        {

        }


        public string Execute(params string[] parameters)
        {
            try
            {

                InputSimulator s = new InputSimulator();
                s.Keyboard.TextEntry("Hello sim !");

                //SendKeys.SendWait("852852852q");
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }
    
}
