using System;
using System.Collections.Generic;
using System.IO;

namespace Common.Observer
{
    public interface IObservable
    {
        // IList<IObserver> observerList = new List<IObserver>();

        IList<IObserver> GetObserverList();

        static void AddObserver(IObserver o)
        {
            throw new NotImplementedException();
        }

        static void RemoveObserver(IObserver o)
        {
            throw new NotImplementedException();
        }

        void AddObserverNonStatic(IObserver o);

        void RemoveObserverNonStatic(IObserver o);

        void MyNotifyAll();

        void MyNotifyAllExcept(IObserver obs);
        
    }
}