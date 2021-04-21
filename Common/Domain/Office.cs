using System;

namespace Common.Domain
{
    [Serializable]
    public class Office: Entity<long>
    {
        private String _location;

        public string Location
        {
            get => _location;
            set => _location = value;
        }

        public Office(long id, string location)
        {
            this.Id = id;
            this._location = location;
        }

        public override string ToString()
        {
            return "{Office: id=" +this.Id+
                   ", location="+this._location +
                   "}";
        }
    }
}