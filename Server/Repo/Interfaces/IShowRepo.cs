using System;
using System.Collections.Generic;
using Common.Domain;

namespace Server.Repo.Interfaces
{
    public interface IShowRepo: IRepository<long, Show>
    {
        IList<Show> FilterByArtist(long artist);
        IList<Show> FilterByDate(DateTime dateTime);
    }
}