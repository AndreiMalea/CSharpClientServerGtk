using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Common.Observer;
using System.Threading;
using Common.Domain;
using Common.Interfaces;

namespace Common.Networking
{
    public class Worker : IObserver
    {
        private IService srv;
        private TcpClient _conn;
        private bool _isConnected = false;

        private NetworkStream _stream;

        public Worker(IService srv, TcpClient conn)
        {
            this.srv = srv;
            this._conn = conn;
            try
            {
                _stream = _conn.GetStream();
                _stream.Flush();

                this._isConnected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }


        public void Run()
        {
            while (_isConnected)
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
                        Response rsp = Handle((Request) buffer.DeSerialize());
                        if (rsp != null)
                        {
                            this.Send(rsp);
                        }
                    }

                    Thread.Sleep(500);
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.StackTrace);
                    this._isConnected = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }

            this.Close();
        }

        private void Send(Response r)
        {
            byte[] buffer = r.Serialize();
            _stream.Write(buffer, 0, buffer.Length);
            _stream.Flush();
        }

        public void Notified()
        {
            this.Send(new ReloadResponse());
        }

        private Response Handle(Request req)
        {
            if (req is LoginRequest)
            {
                Employee loginResult = srv.Login(((LoginRequest) req).User, ((LoginRequest) req).Pass);
                if (loginResult == null)
                {
                    return new Error("Incorrect crediterials!");
                }

                return new LoginResponse(loginResult);
            }
            else if (req is GetAllShowsRequest)
            {
                var lst = srv.GetAllListShows();
                return new GetAllShowsResponse(lst);
            }
            else if (req is FindOneRequest)
            {
                var rsp = srv.FindOneShow(((FindOneRequest) req).Id);

                if (rsp == null)
                {
                    return new Error("Show doesn't exist!");
                }

                return new FindOneResponse(rsp);
            }
            else if (req is GetAllByDateRequest)
            {
                return new GetAllByDateResponse(srv.FilterShowsByDate(((GetAllByDateRequest) req).DateTime));
            }
            else if (req is BuyTicketRequest)
            {
                BuyTicketRequest r = (BuyTicketRequest) req;
                var rsp = srv.BuyTicket(r.Show, r.No, r.Client);
                AbstractObserable.StaticMyNotifyAll();
                return new BuyTicketResponse(rsp);
            }
            else if (req is CloseRequest)
            {
                _isConnected = false;
                // this.Close();
            }

            return null;
        }

        public void Close()
        {
            try
            {
                _stream.Close();
                AbstractObserable.RemoveObserver(this);
                foreach (var observer in AbstractObserable.observerList)
                {
                    Console.WriteLine(observer);
                }

                Console.WriteLine("separator");
                _conn.Close();
                _isConnected = false;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}