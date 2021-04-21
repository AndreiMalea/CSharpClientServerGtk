using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using Common.Interfaces;
using Common.Networking;
using Common.Observer;

namespace Server.Server
{
    public class ConcurrentServer:AbstractServer
    {
        private IService Srv;
        
        public ConcurrentServer(int port, IService srv) : base(port)
        {
            this.Srv = srv;
        }

        protected override Thread GetWorkerThread(TcpClient client)
        {
            Worker w = new Worker(Srv, client);
            AbstractObserable.AddObserver(w);
            return new Thread(w.Run);
        }
    }
}