using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunaTheGlobal.AllCommands
{
    public interface Command
    {
        string Execute(params string[] parameters);

    }
}
