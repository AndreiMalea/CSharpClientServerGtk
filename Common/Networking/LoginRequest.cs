using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common.Networking
{
    [Serializable]
    public class LoginRequest:Request
    {
        private string _user;
        private string _pass;

        public LoginRequest(string user, string pass)
        {
            _user = user;
            _pass = pass;
        }

        public string User
        {
            get => _user;
            set => _user = value;
        }

        public string Pass
        {
            get => _pass;
            set => _pass = value;
        }
    }
}