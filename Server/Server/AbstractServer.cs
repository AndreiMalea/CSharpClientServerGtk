using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Common.Observer;

namespace Server.Server
{
    public abstract class AbstractServer
    {
        private int port;
        private Socket client = null;
        private TcpListener server = null;

        public AbstractServer(int port) {
            this.port = port;
        }

        public void Start() {
            try
            {
                IPHostEntry iphostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = iphostInfo.AddressList[0];
                IPEndPoint localEndpoint = new IPEndPoint(ipAddress, 55556);
                
                client = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                server = new TcpListener(localEndpoint);
                server.Start();
                
                Console.WriteLine("Server started...");
                while (true) {

                    Console.WriteLine("Waiting for clients...");

                    TcpClient cl = server.AcceptTcpClient();
                    
                    Console.WriteLine("Client connected...");

                    Thread thread = this.GetWorkerThread(cl);
                    thread.Start();
                }
            }  catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            } finally {
                this.Stop();
            }
        }

        protected abstract Thread GetWorkerThread(TcpClient client);

        public void Stop(){
            try {
                server.Stop();
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}