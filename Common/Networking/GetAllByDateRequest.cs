using System;

namespace Common.Networking
{
    [Serializable]
    public class GetAllByDateRequest:Request
    {
        private DateTime _dateTime;

        public GetAllByDateRequest(DateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public DateTime DateTime
        {
            get => _dateTime;
            set => _dateTime = value;
        }
    }
}