using ControlRemote.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ControlRemote.VIewModels
{
    public class EntryViewModel 
    {
        private string ip;
        public string IP
        {
            get => ip;
            set { ip = value; }
        }

        public Command OnInputQuery { get; set; }

        public TcpClient client;

        public EntryViewModel()
        {       
            client = new TcpClient();
             OnInputQuery = new Command(InputQuery);

        }

        private  void InputQuery()
        {
            client.Connect(IP, 1234);

            if (client.Connected)
            {
                Preferences.Set("DefaultIP", IP);
                Connection.Instance.client = client;
                App.Current.MainPage = new MainPage(client);
            } 
        }

        private  void InputQuery(string ip)
        {
             client.ReceiveTimeout = 5000;
            try
            {
             client.Connect(ip, 1234);

            }
            catch
            {
                client.Close();
            }

            if (client.Connected)
            {
                Connection.Instance.client = client;
              App.Current.MainPage = new MainPage(client);

            }
           
        }
    }
}
