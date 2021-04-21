using System;
using System.Collections.Generic;
using Common.Domain;

namespace Server.Repo.Interfaces
{
    public interface IOfficeRepo: IRepository<long, Office>
    {
        IList<Office> FilterByLocation(String location);
    }
}