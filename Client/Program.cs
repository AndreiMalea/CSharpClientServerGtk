using System;
using System.Collections.Generic;
using System.Configuration;
using Common.Domain;
using Server.srv;
using Mono.Data.Sqlite;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
// using Server.Repo;
using Client.GUI;
using Common.Networking;
using Gtk;

// using Server.Repo.Interfaces;


namespace Lab1CSharp
{

    static class Program
    {
        private static Configuration GetConfiguration()
        {
            String rv = null;
            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            try
            {
                configMap.ExeConfigFilename =
                    Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "/App.config";
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

            Console.WriteLine(GetConfiguration().AppSettings.Settings["port"].Value);
            var srv = new ClientProxy(int.Parse(GetConfiguration().AppSettings.Settings["port"].Value));

            var app = new Application("org.GtkApplication.GtkApplication", GLib.ApplicationFlags.None);
            Application.Init();
            app.Register(GLib.Cancellable.Current);
            var win = new Login(srv);
            try
            {
                win.App = app;
                Console.WriteLine("am ajuns");
                win.ShowAll();
                Application.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

    }
}