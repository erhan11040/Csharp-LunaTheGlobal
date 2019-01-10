﻿using LunaTheGlobal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LunaTheGlobal.AllCommands
{
    public class DefineProjectPath : PrepareCommand, Command
    {
        public string CommandName = MethodBase.GetCurrentMethod().DeclaringType.Name;

        public DefineProjectPath()
        {

        }


        public string Execute(params string[] parameters)
        {
            try
            {
                if(parameters != null)
                { 
                    Maps.ProjectsPath= parameters[0];
                    return "Success";
                }
                else
                {
                    return "Please give a parameter";
                }
            
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }
}
