using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Socket = System.Net.Sockets.Socket;
using SocketType = System.Net.Sockets.SocketType;
using System.Threading;
using Common.Domain;
using Common.Interfaces;
using Server.Server;
using Server.srv;

namespace Server
{
    class Program
    {
        private static String GetConnectionStringByName(String name)
        {
            String rv = null;
            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            try
            {
                configMap.ExeConfigFilename =
                    Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "/bd.config";
                // configMap.ExeConfigFilename = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "/bd.config";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var set = ConfigurationManager.OpenMappedExeConfiguration(configMap,
                ConfigurationUserLevel.None).ConnectionStrings.ConnectionStrings[name];
            if (set != null)
            {
                rv = set.ConnectionString;
            }

            return rv;
        }
        
        [STAThread]
        public static void Main(string[] args)
        {
            String connectionString = GetConnectionStringByName("SQLite");
            Transaction.IdListInit();

            IService srv = new Service(connectionString);
            AbstractServer server = new ConcurrentServer(55556, srv);
            
            server.Start();
        }

        // IPHostEntry iphostInfo = Dns.GetHostEntry(Dns.GetHostName());
        // IPAddress ipAddress = iphostInfo.AddressList[0];
        // IPEndPoint localEndpoint = new IPEndPoint(ipAddress, 55556);
        //
        //
        // int count = 0;
        //
        // Socket sock = new Socket(ipAddress.AddressFamily,
        //     SocketType.Stream, ProtocolType.Tcp);
        //
        //
        // sock.Bind(localEndpoint);
        // sock.Listen(5);
        //
        //
        // while (true)
        // {
        //
        //     Console.WriteLine("\nWaiting for clients..{0}", count);
        //     Socket confd = sock.Accept();
        //     Thread th = new Thread(Thr);
        //     th.Start(confd);
        // }
        
        // public static void Thr(object? confd)

        // {

        //     byte[] buffer = new byte[1000];

        //     byte[] msg = Encoding.ASCII.GetBytes("From server\n");

        //     string data = null;

        //     int b = ((Socket)confd).Receive(buffer);

        //     data += Encoding.ASCII.GetString(buffer, 0, b);

        //

        //     Console.WriteLine("" + data);

        //     data = null;

        //

        //     ((Socket)confd).Send(msg);

        //     ConsoleKeyInfo key;

        //     Console.WriteLine("\n<< Continue 'y' , Exit 'e'>>");

        //     key = Console.ReadKey();

        //     if (key.KeyChar == 'e')

        //     {

        //         ((Socket)confd).Close();

        //         System.Threading.Thread.Sleep(500);

        //     }

        // }
    }
}