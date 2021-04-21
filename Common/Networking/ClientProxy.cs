using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Common.Domain;
using Common.Interfaces;
using Common.Observer;
using DateTime = System.DateTime;
using Socket = System.Net.Sockets.Socket;
using SocketType = System.Net.Sockets.SocketType;

namespace Common.Networking
{
    public class ClientProxy:IService
    {
        private  int _port;
        private NetworkStream _stream;
        private BlockingCollection<Response> _responses = new BlockingCollection<Response>();
        private Boolean _ended;
        private TcpClient _conn;

        public ClientProxy(int port)
        {
            _port = port;
            this.InitConnection();
        }

        private void InitConnection()
        {
            try
            {
                IPHostEntry iphostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = iphostInfo.AddressList[0];
                IPEndPoint localEndpoint = new IPEndPoint(ipAddress, 55556);
                _conn = new TcpClient();
                
                try
                {
                    _conn.Connect(localEndpoint);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                _stream = _conn.GetStream();

                //lock (_stream)
                _stream.Flush();
                _ended = false;
                Thread th = new Thread(new ThreadStart(this.Run));
                th.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void Send(Request req)
        {
            byte[] x = req.Serialize();
            _stream.Write(x, 0, x.Length);
            _stream.Flush();
        }

        private Response Read()
        {
            Response r = null;
            r = _responses.Take();
            
            return r;
        }
        
        public void Run()
        {
            while(!_ended) {
                try
                {
                    byte[] buffer = new byte[1024];

                    _stream.Read(buffer, 0, buffer.Length);

                    bool isDefault = true;
                    
                    foreach (var b in buffer)
                    {
                        if (!b.Equals(0))
                        {
                            isDefault = false;
                            break;
                        }
                    }
                    
                    if (!isDefault)
                    {
                        var res = buffer.DeSerialize();
                        _responses.Add((Response)res);
                    }
                } catch (Exception e) {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
        
        private class MyThreadStart
        {
               
        }
        
        public IList<IObserver> GetObserverList()
        {
            throw new NotImplementedException();
        }

        public void AddObserver(IObserver o)
        {
            throw new NotImplementedException();
        }

        public void RemoveObserver(IObserver o)
        {
            throw new NotImplementedException();
        }

        public void MyNotifyAll()
        {
            throw new NotImplementedException();
        }

        public void MyNotifyAllExcept(IObserver obs)
        {
            throw new NotImplementedException();
        }

        public IList<Show> GetAllListShows()
        {
            throw new NotImplementedException();
        }

        public Employee EmployeeByUser(string user)
        {
            throw new NotImplementedException();
        }

        public Employee Login(string user, string pass)
        {
            this.Send(new LoginRequest(user, pass));
            Response r = this.Read();

            if (r is LoginResponse)
            {
                return ((LoginResponse) r).Emp;
            }
            else if (r is Error)
            {
                
            }
            
            return null;
        }

        public IList<Show> FilterShowsByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Transaction BuyTicket(Show s, int no, string client)
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployeeByUser(string user)
        {
            throw new NotImplementedException();
        }
    }
}