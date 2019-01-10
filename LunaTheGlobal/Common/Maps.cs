using LunaTheGlobal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunaTheGlobal.Common
{
    public class Maps
    {
        public Maps()
        {
           /* DeviceCommands = "DeviceCommands/" + DeviceName;
            ConfigPath = Mainpath+"/LunaConfig";
            CommandConfigPath = ConfigPath + "/CommandList";*/
        }
        public static string DeviceName = "laptop";
        public static string DevicePassword = "super";
        public static string Mainpath = "C:/LunaTheGlobal";
        public static string ConfigPath= ConfigPath = Mainpath + "/LunaConfig";
        public static string CommandConfigPath= CommandConfigPath = ConfigPath + "/CommandList";
        public static string LunaUrl = "https://lunatheglobal.firebaseio.com/";
        public static string CommandsList = "CommandList";
        public static string DeviceCommands = "DeviceCommands/" + DeviceName;
        public static string LocalDbConnectionString = "Data Source=Luna.sqlite;Version=3;";
        public static string ProjectsPath = Mainpath+"/Projects";
        public static string CurrentProjectName = "LunaTheGlobal";
        public static string CurrentProjectPath = "E:/TümProjeler/" + CurrentProjectName; //ProjectsPath + "/" + CurrentProjectName;
        public static string CurrentLanguage = "C#";
        public static string Pwd = "";
        public static bool IsSleeping = true;
        public static List<string[]> Terminals;
        public static bool Stop = true;
        public static bool Mute = false;
    }
}
