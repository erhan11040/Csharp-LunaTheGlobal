using LunaTheGlobal.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using LunaTheGlobal.Common;
using System.Data.SQLite;
using Windows.UI.Core;
using Windows.Media.SpeechRecognition;
using Windows.Foundation;
using System.Threading;


//using Windows.Media.SpeechSynthesis;

namespace LunaTheGlobal
{
    public partial class Luna : Form
    {

        #region variables
        SpeechSynthesizer speechSyn = new SpeechSynthesizer();
        public Commands commands;
        CommonFunctions common = new CommonFunctions();
        SQLiteConnection Conn = new SQLiteConnection("Data Source=Luna.sqlite;Version=3;");
        private SpeechRecognizer reco = new SpeechRecognizer();
        
        #endregion

        public Luna()
        {
            InitializeComponent();
            // this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            commands = new Commands();//commands template
            Maps.Terminals= common.GetCommandsWithParameters(Conn);
            foreach(var item in common.GetCommandsList(Conn))
            {
                AliasBox.Text += item+"\n";
            }
            StartRecognizeEvent();//voice recognation sets ready
        }
        #region OnClick Methods
        private void startBtn_Click(object sender, EventArgs e)
        {
            Maps.Stop = false;
            Maps.IsSleeping = false;
            stopBtn.Enabled = true;
            startBtn.Enabled = false;
            
            
            
            //voice recognation starts
        }
        private void stopBtn_Click(object sender, EventArgs e)
        {
            Maps.Stop = true;
            startBtn.Enabled = true;
            stopBtn.Enabled = false;
        }
        #endregion

        private void ReadCommands(string result ,string status)
        {
            if (Maps.Stop)
                return;
            
            var b = speechSyn.Voice.Gender;
            string CommandName = "";
            string Answer = "";
            string CommandType = "";
            speechSyn.SelectVoiceByHints(VoiceGender.Female);//female speaks

            if (SpeechBox.InvokeRequired && Maps.IsSleeping == false)
            {
                SpeechBox.Invoke(new MethodInvoker(delegate { SpeechBox.Text = result + "\n" + SpeechBox.Text; }));//shows what user said

            }

            CommandType = common.GetCommandTypeFromAliasInLocal(Conn, result);//get data from db

            if (OldCommandsBox.InvokeRequired && Maps.IsSleeping == false && CommandType!="" && CommandType!=null)
            {
                OldCommandsBox.Invoke(new MethodInvoker(delegate { OldCommandsBox.Text = "Type:" + CommandType + "\n" + OldCommandsBox.Text; }));//shows command

            }
            switch (CommandType)
            {
                case "Chat":
                    Answer = common.GetAnswersInLocal(Conn, result);

                    if (OldCommandsBox.InvokeRequired)
                    {
                        OldCommandsBox.Invoke(new MethodInvoker(delegate { OldCommandsBox.Text = ":" + result + "\n" + OldCommandsBox.Text; }));//shows command

                    }
                    break;
                case "System":
                    CommandName = common.GetCommandNameFromAliasInLocal(Conn, result);

                    if (OldCommandsBox.InvokeRequired)
                    {
                        OldCommandsBox.Invoke(new MethodInvoker(delegate { OldCommandsBox.Text = ":" + CommandName + "  \n" + OldCommandsBox.Text; }));//shows command

                    }
                    Answer = commands.dic[CommandName].Execute();
                    break;
                case "C#":
                    CommandName = common.GetCommandNameFromAliasInLocal(Conn, result);
                    if (OldCommandsBox.InvokeRequired)
                    {
                        OldCommandsBox.Invoke(new MethodInvoker(delegate { OldCommandsBox.Text = ":" + CommandName + "  \n" + OldCommandsBox.Text; }));//shows command

                    }
                    Answer = commands.dic[CommandName].Execute();
                    break;
                default:

                    Dictionary<string, List<string>> CommandNameAndParameters = common.GetCommandNameAndParameters(Conn, result);
                    List<string> Commands = new List<string>();
                    string currentCommandName = "";
                    string[] paramArray = null;
                    foreach (var item in CommandNameAndParameters)
                    {
                        paramArray = item.Value.ToArray();
                        Commands.AddRange(item.Value);
                        currentCommandName = item.Key;

                    }
                    string Answ = string.Join(",", Commands.ToArray());
                    Answ += currentCommandName;
                    if (ResultsBox.InvokeRequired && Maps.IsSleeping == false)
                    {
                        ResultsBox.Invoke(new MethodInvoker(delegate { ResultsBox.Text = Answ + "\n" + ResultsBox.Text; }));//shows answer

                    }
                    try
                    {
                        if (currentCommandName != "")
                            Answer = commands.dic[currentCommandName].Execute(paramArray);
                    }
                    catch (Exception e)
                    {

                    }

                    break;
            }
            if (ResultsBox.InvokeRequired && Maps.IsSleeping == false)
            {
                ResultsBox.Invoke(new MethodInvoker(delegate { ResultsBox.Text = Answer + "\n" + ResultsBox.Text; }));//shows answer

            }
            if (Maps.IsSleeping == false && Maps.Mute==false)
            {
                speechSyn.SpeakAsync(Answer);//speaks..
            }

        }

        #region SpeechRecognize
        private async void StartRecognizeEvent()
        {
            IAsyncOperation<SpeechRecognitionCompilationResult> op = reco.CompileConstraintsAsync();
            op.Completed += HandleCompilationCompleted;
        }

        private void RecogComplated(IAsyncOperation<SpeechRecognitionResult> asyncInfo, AsyncStatus asyncStatus)
        {
            var result = asyncInfo.GetResults();//result of speech
            var status = asyncStatus.ToString();//succes or failed
            ReadCommands(result.Text, status.ToString());
            IAsyncOperation<Windows.Media.SpeechRecognition.SpeechRecognitionResult> speechRecognitionResult = reco.RecognizeAsync();
            speechRecognitionResult.Completed += RecogComplated;//restarts recognation
        }

        public async  void HandleCompilationCompleted(IAsyncOperation<SpeechRecognitionCompilationResult> opInfo, AsyncStatus status)
        {
            if (status == AsyncStatus.Completed)
            {
                
                System.Diagnostics.Debug.WriteLine("CompilationCompleted");
                var result = opInfo.GetResults();
                
                System.Diagnostics.Debug.WriteLine(result.ToString());

                IAsyncOperation<Windows.Media.SpeechRecognition.SpeechRecognitionResult> speechRecognitionResult = reco.RecognizeAsync();
                speechRecognitionResult.Completed += RecogComplated;
            }
        }
        #endregion

        #region ListensNewCommands , executes and answers
        //fire base 
        public void ListenCommands()
       {
            var childPath = Maps.DeviceCommands;
            var firebase = new FirebaseClient(Maps.LunaUrl);
            var observable = firebase
              .Child(childPath)
              .OrderByKey()
              .LimitToFirst(5)
              .AsObservable<DeviceCommands>()
              .Subscribe(a => NewCommand(a.Object,childPath+"/"+ a.Key));

        }
        public void NewCommand(DeviceCommands obj,string path)
        {
            if(obj.Answer=="0")
            {

                //its a new command
                if (AliasBox.InvokeRequired)
                {
                    AliasBox.Invoke(new MethodInvoker(delegate { AliasBox.Text += "Command: "+obj.CommandText +" FROM: "+obj.From ; }));
                    
                }
                obj.Answer = commands.ExecuteCommand(obj.CommandName);
                SetAnswer(obj, path);
            }
        }
        public async void SetAnswer(DeviceCommands obj, string childPath)
        {
            var firebase = new FirebaseClient(Maps.LunaUrl);
            await firebase
              .Child(childPath)
              .PutAsync(obj);
        }




        #endregion

        
    }


}
