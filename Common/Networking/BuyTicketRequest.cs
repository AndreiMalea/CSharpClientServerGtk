using System;
using Common.Domain;

namespace Common.Networking
{
    [Serializable]
    public class BuyTicketRequest:Request
    {
        private Show _show;
        private int _no;
        private string _client;

        public Show Show
        {
            get => _show;
            set => _show = value;
        }

        public int No
        {
            get => _no;
            set => _no = value;
        }

        public string Client
        {
            get => _client;
            set => _client = value;
        }

        public BuyTicketRequest(Show show, int no, string client)
        {
            _show = show;
            _no = no;
            _client = client;
        }
    }
}