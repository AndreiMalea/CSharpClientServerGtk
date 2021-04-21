using System;
using System.Collections.Generic;
using Common.Domain;

namespace Server.Repo.Interfaces
{
    public interface IArtistRepo: IRepository<long, Artist>
    {
        IList<Artist> FilterByName(String name);
        IList<Artist> FilterByGenre(String genre);
    }
}