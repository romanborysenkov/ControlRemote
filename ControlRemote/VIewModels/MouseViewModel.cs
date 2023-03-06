using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ControlRemote.VIewModels
{
    public class MouseViewModel : CommandsViewModel
    {

        private TcpClient tcpclient;
        public TcpClient ClientTcp
        {
            get => tcpclient;
            set => tcpclient = value;
        }

        public MouseViewModel(TcpClient client):base(client)
        {
           
             ClientTcp = client;
           
        }

       
    }
}
