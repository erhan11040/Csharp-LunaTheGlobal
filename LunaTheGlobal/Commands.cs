using LunaTheGlobal.AllCommands;
using LunaTheGlobal.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LunaTheGlobal
{
    public class Commands
    {
        public Dictionary<string, Command> dic= new Dictionary<string, Command>();
        
        public Commands()
        {
           
            CommonFunctions commonFunctions = new CommonFunctions();
            commonFunctions.CreateFolder(Maps.Mainpath);
            commonFunctions.CreateFolder(Maps.ConfigPath,true);
            commonFunctions.CreateFolder(Maps.CommandConfigPath, true);
            commonFunctions.CreateFolder(Maps.ProjectsPath);

            AddCommandsToDic();
            
        }
        public string ExecuteCommand(string commandName)
        {
            Command obj =dic[commandName];
            obj.Execute();
            return "";
        }
        private void AddCommandsToDic()
        {
            CreateNewFolder     createNewFolder = new CreateNewFolder();
            CreateNewFile         createNewFile = new CreateNewFile();
            LockPc                       lockPc = new LockPc();
            Shutdown                   shutDown = new Shutdown();
            SearchInPc               searchInPc = new SearchInPc();
            UnlockPc                   unlockPc = new UnlockPc();
            SleepPc                     sleepPc = new SleepPc();
            SleepLuna                 sleepLuna = new SleepLuna();
            WakeUpLuna               wakeUpLuna = new WakeUpLuna();
            CreateClass             createClass = new CreateClass();
            DefineLanguage       defineLanguage = new DefineLanguage();
            DefineProjectName defineProjectName = new DefineProjectName();
            DefineProjectPath defineProjectPath = new DefineProjectPath();
            GoTo                           goTo = new GoTo();
            WhereAmI                   whereAmI = new WhereAmI();


            dic.Add (createNewFile.CommandName      ,   createNewFile);
            dic.Add (createNewFolder.CommandName    ,   createNewFolder);
            dic.Add (lockPc.CommandName             ,   lockPc);
            dic.Add (shutDown.CommandName           ,   shutDown);
            dic.Add (searchInPc.CommandName         ,   searchInPc);
            dic.Add (unlockPc.CommandName           ,   unlockPc);
            dic.Add (sleepPc.CommandName            ,   sleepPc);
            dic.Add (sleepLuna.CommandName          ,   sleepLuna);
            dic.Add (wakeUpLuna.CommandName         ,   wakeUpLuna);
            dic.Add (createClass.CommandName        ,   createClass);
            dic.Add (defineLanguage.CommandName     ,   defineLanguage);
            dic.Add (defineProjectName.CommandName  ,   defineProjectName);
            dic.Add (defineProjectPath.CommandName  ,   defineProjectPath);
            dic.Add (goTo.CommandName               ,   goTo);
            dic.Add (whereAmI.CommandName           ,   whereAmI);

        }

        
    }
    
    
    
    
}
