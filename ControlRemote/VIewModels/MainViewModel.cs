using ControlRemote.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
//using ThreadNetwork;

namespace ControlRemote.VIewModels
{
   // [QueryProperty(nameof(client), nameof(client))]
    public class MainViewModel : CommandsViewModel
    {
        private string que;
        public string Query
        {
            get => que;
            set { que = value; }
        }


        public Command OnInputQuery { get; set; }

        public Command OnOpenFind { get; set; }



        private TcpClient tcpclient;


        public TcpClient Client
        {
            get=> tcpclient;
            set => tcpclient = value;
        }

        public MainViewModel(TcpClient client = null) : base(client)
        {
            OnOpenFind = new Command(OpenFind);
            OnInputQuery = new Command(InputQuery);

            this.Client = client;

        }


        public void Close()
        {
            NetworkStream stream = Client.GetStream();

            string s = "closethread";
            byte[] message = Encoding.ASCII.GetBytes(s);
            stream.Write(message, 0, message.Length);

            Client.Client.Close();
        }

        private  void InputQuery()
        {
            if (!Client.Connected) Client.Connect(Preferences.Get("DefaultIP",""), 1234);

            if (Client.Connected)
            {
                Connection.Instance.client = Client;
            }

            NetworkStream stream = Client.GetStream();

            
            byte[] message = Encoding.Unicode.GetBytes(Query);
            stream.Write(message, 0, message.Length);

           
        }
        private void OpenFind()
        {
               if(!Client.Connected)  Client.Connect(Preferences.Get("DefaultIP",""), 1234);

                if (Client.Connected)
                {
                    Connection.Instance.client = Client;
                }
            NetworkStream stream = Client.GetStream();

            string s = "findwin";
            byte[] message = Encoding.Unicode.GetBytes(s);
            stream.Write(message, 0, message.Length);
            
        }
       
    }
}
