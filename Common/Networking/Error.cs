using System;

namespace Common.Networking
{
    [Serializable]
    public class Error:Response
    {
        private String msg;

        public Error(string msg)
        {
            this.msg = msg;
        }

        public string Msg
        {
            get => msg;
            set => msg = value;
        }
    }
}