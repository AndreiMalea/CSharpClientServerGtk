using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Common.Domain;
using Common.Networking;

namespace Common
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                IPHostEntry iphostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAdress = iphostInfo.AddressList[0];
                IPEndPoint ipEndpoint = new IPEndPoint(ipAdress, 55556);
                
                TcpClient client = new TcpClient();
                client.Connect(ipEndpoint);
            
                Console.WriteLine("Socket created to {0}", client.Connected);
            
            
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
            // Console.WriteLine("Transmission end.");
            // Console.ReadKey();

        }
    }
}