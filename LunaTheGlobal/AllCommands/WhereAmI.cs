using LunaTheGlobal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LunaTheGlobal.AllCommands
{
    public class WhereAmI : PrepareCommand, Command
    {
        public WhereAmI()
        {

        }
        public string CommandName = MethodBase.GetCurrentMethod().DeclaringType.Name;
        public string Execute(params string[] parameters)
        {
            try
            {
              

                return Maps.Pwd;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }
}
