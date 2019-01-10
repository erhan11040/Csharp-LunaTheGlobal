using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunaTheGlobal.Common
{
    public class Search
    {
        List<string> dirs = new List<string>();
        public void DirSearch(string driver,string file)
        {
            try
            {
                
                int a = 0;
                foreach (string d in Directory.GetDirectories(driver))
                {
                    foreach (string f in Directory.GetFiles(d, file))
                    {
                        a++;
                        dirs.Add(f);
                    }
                    DirSearch(d,file);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }
        public List<string> SearchFile(string whatToSearch,string type="" , string path="file:C:/")
        {
            List<string> results = new List<string>();
            try
            {
                
                var query="";
                // File name search (case insensitive), also searches sub directories
                var query1 = @"SELECT System.ItemUrl  FROM SystemIndex " +
                            @"WHERE scope ='"+path+"' AND System.ItemName LIKE '%" + whatToSearch + "%'";

                // File name search (case insensitive), does not search sub directories
                var query2 = @"SELECT System.ItemName FROM SystemIndex " +
                            @"WHERE directory = '" + path + "' AND System.ItemName LIKE '%" + whatToSearch + "%' ";

                // Folder name search (case insensitive)
                var query3 = @"SELECT System.ItemName FROM SystemIndex " +
                            @"WHERE scope = '" + path + "' AND System.ItemType = 'Directory' AND System.Itemname LIKE '%" + whatToSearch + "%' ";

                // Folder name search (case insensitive), does not search sub directories
                var query4 = @"SELECT System.ItemName FROM SystemIndex " +
                            @"WHERE directory = '" + path + "' AND System.ItemType = 'Directory' AND System.Itemname LIKE '%" + whatToSearch + "%' ";

                switch (type)
                {
                    case "File":
                        query = query1;
                        break;
                    case "FileUnSub":
                        query = query2;
                        break;
                    case "Folder":
                        query = query3;
                        break;
                    case "FolderUnSub":
                        query = query4;
                        break;
                    default:
                        query = query1;
                        break;
                }
                


                var connection = new OleDbConnection(@"Provider=Search.CollatorDSO;Extended Properties=""Application=Windows""");

                connection.Open();

                var command = new OleDbCommand(query, connection);
                string result;
                
                using (var r = command.ExecuteReader())
                {
                    while (r.Read())
                    {
                        
                        result = r[0].ToString();
                        results.Add(result);
                    }
                }

                connection.Close();


                /*string rootDirectory = System.IO.DriveInfo.GetDrives()[0].RootDirectory.FullName;

                string[] files = System.IO.Directory.GetFiles(
                            rootDirectory,
                            "vi.mwb", System.IO.SearchOption.AllDirectories);*/
                return results;
            }
            catch (Exception e)
            {
                return results;
            }
        }

       
    }
}
