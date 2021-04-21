using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common.Domain
{
    [Serializable]
    public class Artist: Entity<long>
    {
        private String _name;
        private String _genre;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Genre
        {
            get => _genre;
            set => _genre = value;
        }

        public Artist(long id, string name, string genre)
        {
            this.Id = id;
            _name = name;
            _genre = genre;
        }

        public override string ToString()
        {
            return "{Artist : id="+this.Id+", name="+this.Name+", genre="+this.Genre+"}";
        }
        
        //
        
    }
}