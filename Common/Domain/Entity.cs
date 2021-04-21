using System;
using System.Runtime.Serialization;

namespace Common.Domain
{
    [Serializable]
    public abstract class Entity<ID>
    {
        private ID _id;

        public ID Id
        {
            get => _id;
            set => _id = value;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}