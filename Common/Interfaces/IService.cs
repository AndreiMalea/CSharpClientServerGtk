using System;
using System.Collections.Generic;
using Common.Domain;
using Common.Observer;

namespace Common.Interfaces
{
    public interface IService:IObservable
    {
        public Show FindOneShow(long id);
        
        IList<Show> GetAllListShows();

        Employee Login(string user, string pass);

        IList<Show> FilterShowsByDate(DateTime date);

        Transaction BuyTicket(Show s, int no, string client);

        void Close(IObserver o);
    }
}