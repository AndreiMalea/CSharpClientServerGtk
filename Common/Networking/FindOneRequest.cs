using System;

namespace Common.Networking
{
    [Serializable]
    public class FindOneRequest:Request
    {
        private long id;

        public long Id
        {
            get => id;
            set => id = value;
        }

        public FindOneRequest(long id)
        {
            this.id = id;
        }
    }
}