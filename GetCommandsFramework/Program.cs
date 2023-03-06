using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows;
using System.Runtime.CompilerServices;
using System.Net.NetworkInformation;
using System.Threading;

using System.Runtime.InteropServices;
using System.Reflection.Metadata;
using GetCommandsFramework;
using static GetCommandsFramework.MouseControl;

namespace GetCommandsFramework
{
    internal class Program
    {
        static  void Main(string[] args)
        {

            Thread keyboardThread = new Thread(StartKeyboardChecking);
            Thread mouseThread = new Thread(StartMouseChecking);

            keyboardThread.Start();
            mouseThread.Start(); 
        }
      
        static void StartKeyboardChecking()
        {

        KeyboardReConnect:
            TcpClient client;
            TcpListener listener;
            string ipString = "";

            IPAddress[] localIp = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress address in localIp)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipString = address.ToString();
                }
            }

             IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ipString), 1234);
            listener = new TcpListener(ep);
            listener.Start();

            Console.WriteLine($"Started listening request at {ep.Address} : {ep.Port} - It's a keyboard");
                client = listener.AcceptTcpClient();

            if(client.Connected)
            {
                Console.WriteLine("The keyboard are successed connected!");
            }

            do
            {  
           
            
                const int bytesize = 1024 * 1024;
                byte[] buffer = new byte[bytesize];
                int x = client.GetStream().Read(buffer, 0, bytesize);
                
                var data = ASCIIEncoding.Unicode.GetString(buffer);
               
                string result = "";
               
                for(int i=0;i<x/2;i++)
                {
                    result += data[i];
                }

                switch(result.ToLower())
                {
                    case "": Console.WriteLine("Connection for keyboard is losted");  goto KeyboardReConnect; 
                   
                    case "findwin": OpenFind();
                        break;
                        
                    default: SendKeys.SendWait(result); 
                        break;
                    
                }
   
            } while (client.Connected); 
        }

        static void StartMouseChecking()
        {
            MouseReConnect:

              TcpClient client;
            TcpListener listener;
            string ipString = "";

            IPAddress[] localIp = Dns.GetHostAddresses(Dns.GetHostName());
                foreach (IPAddress address in localIp)
                {
                    if (address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipString = address.ToString();
                    }
                }

                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ipString), 5555);
                listener = new TcpListener(ep);
                listener.Start();

                Console.WriteLine($"Started listening request at {ep.Address} : {ep.Port} - It's a mouse");
                client = listener.AcceptTcpClient();

                if (client.Connected)
                {
                    Console.WriteLine("The mouse are successed connected!");
                }

                do
                {

                    const int bytesize = 1024 * 1024;
                    byte[] buffer = new byte[bytesize];
                    int x = client.GetStream().Read(buffer, 0, bytesize);

                    var data = ASCIIEncoding.ASCII.GetString(buffer);

                string result = "";

                for (int i = 0; i < x; i++)
                {
                    result += data[i];
                }


                switch (result)
                {
                    case "": Console.WriteLine("Connection for mouse is losted"); goto MouseReConnect;
                    case "{DOWN}":
                        MoveMouse(0,10);
                        break;
                    case "{UP}":
                        MoveMouse(0, -10);
                        break;

                    case "{LEFT}":
                        MoveMouse(-10, 0);
                        break;

                    case "{RIGHT}":
                        MoveMouse(10, 0);
                        break;
                    case "{ENTER}":
                        DoMouseLeftClick(Cursor.Position.X, Cursor.Position.Y);
                        break;
                }


                } while (client.Connected);
            
        }
    }
}
