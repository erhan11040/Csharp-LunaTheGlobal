using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LunaTheGlobal.Common
{
    public class CommonFunctions
    {
       

        #region system works
        public void CreateFolder( string path,bool isHidden=false)
        {
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                if(isHidden)
                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
        }
        public void CreateFile(string path)
        {
            try
            {
                StreamWriter Dosya = File.CreateText(path);
                
            }
            catch (Exception e)
            {
                string error= e.Message;
            }
        }
        public static string[] Spliter(string s, string separator)
        {
            return s.Split(new string[] { separator }, StringSplitOptions.None);
        }
        public string[] GetBetweenStrings(string str, Dictionary<string,int> Parameters)
        {
           


            
            
            foreach (var item in Parameters)
            {
                if(item.Key!="")
                    str = str.Replace(item.Key.ToLower() , "|");
            }
            string[] Return = Spliter(str, "|");

            return Return;
        }
        #endregion

        #region db and commnad calls...
        private int GetAnswersCount(SQLiteConnection Conn, string alias)
        {
            alias = alias.Replace("'", "*");
            Conn.Open();
            int result = 0; ;
            string sql2 = "Select Count(*)as cnt From ChatAnswers Where CommandId=(Select CommandId from Alias Where CommandAlias LIKE '%" + alias + "%')";
            SQLiteCommand command2 = new SQLiteCommand(sql2, Conn);
            SQLiteDataReader reader = command2.ExecuteReader();
            
            while (reader.Read())
                result = Convert.ToInt32( reader["cnt"]);
            Conn.Close();
            return result;
        }
        public string GetAnswersInLocal(SQLiteConnection Conn, string alias)
        {
            //not complated models gonna be created and return types of these functions ll be object type of models...
            alias = alias.Replace("'", "*");
            string[] CommandsText = new string[GetAnswersCount(Conn,alias)];
            Conn.Open();
            if(alias=="")
            {
                return null;
            }
            string sql2 = "Select Answer From ChatAnswers Where CommandId=(Select CommandId from Alias Where CommandAlias  LIKE '%" + alias + "%')";
            SQLiteCommand command2 = new SQLiteCommand(sql2, Conn);
            SQLiteDataReader reader = command2.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                CommandsText[i] = reader["Answer"].ToString();
                i++;
            }
                
            Conn.Close();
            int seed = (int)DateTime.Now.Ticks;
            Random ran = new Random(seed);
            int randomInt = ran.Next(0, CommandsText.Length-1);
            return CommandsText[randomInt]; 
        }
        public string GetCommandTypeFromAliasInLocal(SQLiteConnection Conn, string alias)
        {
            if (alias == "")
            {
                return null;
            }
            alias =alias.Replace("'", "*");
            string CommandType = "";
            Conn.Open();
            string sql2 = "Select TypeName From CommandTypes Where id=( Select CommandTypeId From Commands Where id=(Select CommandId from Alias Where CommandAlias LIKE '%" + alias + "%' LIMIT 1))";
            SQLiteCommand command2 = new SQLiteCommand(sql2, Conn);
            SQLiteDataReader reader = command2.ExecuteReader();
            while (reader.Read())
                CommandType = reader["TypeName"].ToString();
            Conn.Close();
            return CommandType;
        }
        public string GetCommandNameFromAliasInLocal(SQLiteConnection Conn ,string alias )
        {
            alias = alias.Replace("'", "*");
            string CommandName="";
            Conn.Open();
            string sql2 = "Select CommandName From Commands Where id=(Select CommandId from Alias Where CommandAlias LIKE '%" + alias + "%' LIMIT 1)";
            SQLiteCommand command2 = new SQLiteCommand(sql2, Conn);
            SQLiteDataReader reader = command2.ExecuteReader();
            while (reader.Read())
                CommandName = reader["CommandName"].ToString();
            Conn.Close();
            return CommandName;
        }
        public List<string[]> GetCommandsWithParameters(SQLiteConnection Conn)
        {

            List<string> Alias = new List<string>();
            List<string[]> Terminals = new List<string[]>();
            string Seperator = "...";
            Conn.Open();
            string sql2 = "Select CommandAlias from Alias Where CommandAlias LIKE '%" + Seperator + "%' ";
            SQLiteCommand command2 = new SQLiteCommand(sql2, Conn);
            SQLiteDataReader reader = command2.ExecuteReader();
            while (reader.Read())
            {
                Alias.Add(reader["CommandAlias"].ToString());
            }
            foreach(var item in Alias)
            {
                Terminals.Add(Spliter(item,Seperator));
            }
            Conn.Close();

            return Terminals;
        }
        public List<Dictionary<string, int>> ArrangeTerminals (string CommandAlias)
        {
            List<Dictionary<string, int>> TerminalIndex = new List<Dictionary<string, int>>();
            int index=0;
            int size=0;
            CommandAlias = CommandAlias.ToLower();
            foreach (var item in Maps.Terminals )
            {
                //List<int> CurrentTerminalIndex = new List<int>();
                Dictionary<string, int> CurrentTerminalIndex = new Dictionary<string, int>();
                index = 0;
                foreach(var Terminal in item)
                {
                    size = item.Count();
                    if (Terminal == "" || Terminal == " ")
                        continue;
                    index = CommandAlias.IndexOf(Terminal.ToLower());
                    if (index == -1)
                        break;
                    else
                    {
                        
                        CurrentTerminalIndex.Add(Terminal, index);
                    }
                        
                }

                if(index!= -1 && size!=0)
                    TerminalIndex.Add(CurrentTerminalIndex);
                
                   
            }
            
            
            return TerminalIndex;
        }
        public Dictionary<string, List<string>> GetCommandNameAndParameters (SQLiteConnection Conn, string Alias)
        {
            Alias = Alias.ToLower();
            List<Dictionary<string, int>> TerminalIndex = new List<Dictionary<string, int>>();
            Dictionary<string, int> SelectedTerminal = new Dictionary<string, int>();
            
            Dictionary<string, List<string>> CommandNameAndParamDic = new Dictionary<string, List<string>>();
            TerminalIndex = ArrangeTerminals(Alias);
            var dictSize = 0;
            foreach(var item in TerminalIndex)
            {
                
                if(dictSize>=item.Count)
                {

                }
                else
                {
                    dictSize = item.Count;
                    SelectedTerminal = item;
                }
                    
            }
            string CommandAlias="";
            string previusTerminal = "";
            int previusTerminalIndex = -1;
            var list = new List<string>();
            foreach (var terminal in SelectedTerminal)
            {
                
                if(terminal.Value == 0)
                {
                    CommandAlias = terminal.Key;
                    if (SelectedTerminal.Count() == 1)
                        CommandAlias += "...";
                }
                else
                {
                    CommandAlias += "..."+terminal.Key;
                }
                if(previusTerminal!="" && previusTerminalIndex!= -1 )
                {
                    
                }
                previusTerminal = terminal.Key;
                previusTerminalIndex = terminal.Value;
                //split at
            }
            
            list.AddRange(GetBetweenStrings(Alias,SelectedTerminal));
            string CommandName=GetCommandNameFromAliasInLocal(Conn,CommandAlias);
            int index = CommandAlias.IndexOf("...");
            if (index < 0)
                CommandName = "";
            CommandNameAndParamDic.Add(CommandName, list);
            return CommandNameAndParamDic;  
        }

        public List<string> GetCommandsList(SQLiteConnection Conn)
        {
            List<string> result = new List<string>();
            Conn.Open();
            string sql2 = "Select CommandName ,CommandAlias From Commands,Alias Where Alias.CommandId=Commands.id order by CommandName  ";
            SQLiteCommand command2 = new SQLiteCommand(sql2, Conn);
            SQLiteDataReader reader = command2.ExecuteReader();
            while (reader.Read())
              result.Add( reader["CommandName"].ToString() +">>"+reader["CommandAlias"].ToString());
            Conn.Close();
            return result ;
        }
        

        #endregion


        #region db examples
        public void DbInsert(SQLiteConnection conn)
        {
            conn.Open();
            string sql3 = "insert into highscores (name, score) values ('Me2', 3200)";
            SQLiteCommand command3 = new SQLiteCommand(sql3, conn);
            command3.ExecuteNonQuery();
            conn.Close();
        }
        public void DbCreateTable(SQLiteConnection conn)
        {
            conn.Open();
            string sql = "create table highscores (name varchar(20), score int)";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            conn.Close();
        }
        public void CreateDb()
        {
             SQLiteConnection.CreateFile("MyDatabase.sqlite");
        }
        public void DbSelect(SQLiteConnection conn)
        {
            string result="";
            conn.Open();
            string sql2 = "select * from highscores order by score desc";
            SQLiteCommand command2 = new SQLiteCommand(sql2, conn);
            SQLiteDataReader reader = command2.ExecuteReader();
            while (reader.Read())
                result += "Name: " + reader["name"] + "\tScore: " + reader["score"];
            conn.Close();
        }
        #endregion
    }
}
