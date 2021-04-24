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
using Common.Observer;
using Server.Server;
using Server.srv;

namespace Server
{
    class Program
    {
        private static Configuration GetConfiguration()
        {
            
            AbstractObserable.InitObservable();
            String rv = null;
            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            try
            {
                configMap.ExeConfigFilename =
                    Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "/bd.config";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var set = ConfigurationManager.OpenMappedExeConfiguration(configMap,
                ConfigurationUserLevel.None);
            
            return set;
        }
        
        [STAThread]
        public static void Main(string[] args)
        {
            Transaction.IdListInit();
            IService srv = new Service(GetConfiguration().ConnectionStrings.ConnectionStrings["SQLite"].ConnectionString);
            AbstractServer server = new ConcurrentServer(int.Parse(GetConfiguration().AppSettings.Settings["port"].Value), srv);
            
            server.Start();
        }
    }
}