using System;
using System.Collections.Generic;
using Common.Domain;
using Common.Observer;

namespace Common.Interfaces
{
    public interface IService:IObservable
    {
        
        IList<Show> GetAllListShows();

        Employee EmployeeByUser(string user);

        Employee Login(string user, string pass);

        IList<Show> FilterShowsByDate(DateTime date);

        Transaction BuyTicket(Show s, int no, string client);

        Employee GetEmployeeByUser(string user);
    }
}