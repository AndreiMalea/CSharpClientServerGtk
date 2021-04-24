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
    public class ClientProxy : IService
    {
        private int _port;
        private NetworkStream _stream;
        private BlockingCollection<Response> _responses = new BlockingCollection<Response>();
        private Boolean _ended;
        private TcpClient _conn;
        private IList<IObserver> _observers;

        public ClientProxy(int port)
        {
            Console.WriteLine("new proxy created");
            _port = port;
            _observers = new List<IObserver>();
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
            while (!_ended)
            {
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
                        var resp = (Response) res;

                        if (resp is ReloadResponse)
                        {
                            MyNotifyAll();
                        }
                        else
                        {
                            _responses.Add(resp);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        public void Close(IObserver o)
        {
            if (o != null)
            {
                this.RemoveObserverNonStatic(o);
            }
            this.Send(new CloseRequest());
            this._ended = true;
        }
        
        public IList<IObserver> GetObserverList()
        {
            return _observers;
        }

        public void AddObserverNonStatic(IObserver o)
        {
            if (!_observers.Contains(o))
            {
                foreach (var observer in _observers)
                {
                    Console.WriteLine(observer);
                }
            
                _observers.Add(o);
            }
        }

        public void RemoveObserverNonStatic(IObserver o)
        {
            if (_observers.Contains(o))
            {
                _observers.Remove(o);
            }
        }

        public void MyNotifyAll()
        {
            foreach (var observer in _observers)
            {
                Console.WriteLine(observer);
                observer.Notified();
            }
        }

        public void MyNotifyAllExcept(IObserver obs)
        {
            throw new NotImplementedException();
        }

        public IList<Show> GetAllListShows()
        {
            this.Send(new GetAllShowsRequest());
            Response r = this.Read();

            if (r is GetAllShowsResponse)
            {
                Console.WriteLine(((GetAllShowsResponse) r).Shows);
                return ((GetAllShowsResponse) r).Shows;
            }

            return null;
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
                throw new Exception(((Error) r).Msg);
            }

            return null;
        }

        public IList<Show> FilterShowsByDate(DateTime date)
        {
            this.Send(new GetAllByDateRequest(date));
            Response r = this.Read();

            if (r is GetAllByDateResponse)
            {
                return ((GetAllByDateResponse)r).Shows;
            }
            else if (r is Error)
            {
                throw new Exception(((Error) r).Msg);
            }

            return null;
        }

        public Show FindOneShow(long id)
        {
            this.Send(new FindOneRequest(id));
            Response r = this.Read();

            if (r is FindOneResponse)
            {
                return ((FindOneResponse) r).Show;
            }
            else if (r is Error)
            {
                throw new Exception(((Error) r).Msg);
            }

            return null;
        }

        public Transaction BuyTicket(Show s, int no, string client)
        {
            this.Send(new BuyTicketRequest(s, no, client));
            Response r = this.Read();

            if (r is BuyTicketResponse)
            {
                return ((BuyTicketResponse) r).Transaction;
            }
            else if (r is Error)
            {
                throw new Exception(((Error) r).Msg);
            }

            return null;
        }

    }
}