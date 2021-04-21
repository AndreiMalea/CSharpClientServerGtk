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
    public class Worker:IObserver
    {
        private IService srv;
        private TcpClient _conn;
        private bool _isConnected = false;
        private NetworkStream _stream;
        // private NetworkStream _output;

        public Worker(IService  srv, TcpClient conn)
        {
            this.srv = srv;
            this._conn = conn;
            try
            {
                // _input = new NetworkStream(_conn, FileAccess.Read);
                // _output = new NetworkStream(_conn, FileAccess.Write);
                // _output.Flush();

                _stream = _conn.GetStream();
                // lock (_stream)
                _stream.Flush();
                
                this._isConnected = true;
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void Run() {
            while (_isConnected) {
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
                } catch (IOException e) {
                    Console.WriteLine(e.StackTrace);
                    this._isConnected = false;
                } catch (Exception e) {
                    Console.WriteLine(e.StackTrace);
                }
            }

            this.Close();
        }

        private void Send(Response r)
        {
            // lock (_stream)
            byte[] buffer = r.Serialize();
            _stream.Write(buffer, 0, buffer.Length);
            _stream.Flush();
            // _stream.BeginWrite(r.Serialize(), 0, r.Serialize().Length, delegate(IAsyncResult ar)
            // {
            //     _stream.EndWrite(ar);
            //     _stream.FlushAsync();
            //     return;
            // }, null);
            // _output.Write(r.Serialize());
            // _output.Flush();
        }
        
        public void Notified()
        {
            throw new NotImplementedException();
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

            return null;
        }
        
        public void Close() {
            try {
                // lock (_stream)
                _stream.Close();
                AbstractObserable.RemoveObserver(this);
                _conn.Close();
                _isConnected = false;
            } catch (IOException e) {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}