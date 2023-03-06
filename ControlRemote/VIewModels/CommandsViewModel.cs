using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ControlRemote.VIewModels
{
    public class CommandsViewModel
    {
        public Command OnDownCommand { get; set; }

        public Command OnUpCommand { get; set; }

        public Command OnLeftCommand { get; set; }

        public Command OnRightCommand { get; set; }

        public Command OnEnterCommand { get; set; }

        public Command OnDelete { get; set; }
        public Command OnFind { get; set; }


        private TcpClient tcpclient;
        public TcpClient Client
        {
            get => tcpclient;
            set => tcpclient = value;
        }

        NetworkStream stream;

      public  bool animationInProgress = false;
     public Stopwatch stopwatch = new Stopwatch();

        public CommandsViewModel(TcpClient client)
        {
            OnDelete = new Command(async () => await SendKeyboardCommand("{BACKSPACE}"));
            OnFind = new Command(async () => await SendKeyboardCommand("^(f)"));
            OnDownCommand = new Command(async()=>await SendKeyboardCommand("{DOWN}"));
            OnUpCommand = new Command(async () => await SendKeyboardCommand("{UP}"));
            OnLeftCommand = new Command(async () => await SendKeyboardCommand("{LEFT}"));
            OnRightCommand = new Command(async () => await SendKeyboardCommand("{RIGHT}"));
            OnEnterCommand = new Command(async() =>await SendKeyboardCommand("{ENTER}"));

            
            
            this.Client = client;

            stream = client.GetStream();
        }

        private async Task SendKeyboardCommand(string comm)
        {
           
            byte[] message = Encoding.Unicode.GetBytes(comm);
            stream.Write(message, 0, message.Length);
                 
        }

        [Obsolete]
        public  void SendCommand(string comm)
         {
            stopwatch.Start();
            animationInProgress = true;
            byte[] message = Encoding.ASCII.GetBytes(comm);

            Device.StartTimer(TimeSpan.FromMilliseconds(50), () =>
            {
                stream.Write(message, 0, message.Length);
                return animationInProgress;
            });

           
        }
    }
}
