using LunaTheGlobal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunaTheGlobal.Languages
{
    public class CSharp
    {
        public static string DefaultLibraries = "using System;\n"+
                                         "using System.Collections.Generic;\n"+
                                         "using System.Linq;\n"+
                                         "using System.Text;\n"+
                                         "using System.Threading.Tasks;\n";
        public static string LibraryFilepath=  System.IO.Directory.GetCurrentDirectory()+ "/ProgrammingLibrary"+"/Csharp";
        public static string ClassAccessLevel = "public";
        public static string ClassName = "name";
        public static string ClassSyntax = CSharp.ClassAccessLevel + "class" + ClassName ;
        public static string NameSpaceSyntax = "namespace "+Maps.CurrentProjectName;
    }
}
