using System;
using System.Collections.Generic;
using System.IO;

namespace Common.Observer
{
    public abstract class AbstractObserable : IObservable
    {
        private static IList<IObserver> observerList = null;

        public static void InitObservable()
        {
            observerList = new List<IObserver>();
        }
        
        public IList<IObserver> GetObserverList() {
            return observerList;
        }

        public static void AddObserver(IObserver o) {
            lock (observerList)
            {
                observerList.Add(o);
                foreach (var observer in observerList)
                {
                    Console.WriteLine(observer); 
                }
            }
        }

        public static void RemoveObserver(IObserver o) {
            lock (observerList) {
                observerList.Remove(o);
            }
        }

        public void MyNotifyAll()
        {
            foreach (var observer in observerList)
            {
                try {
                    observer.Notified();
                } catch (IOException ioException) {
                    Console.Error.Write(ioException.StackTrace);
                }
            }
        }

        public void MyNotifyAllExcept(IObserver obs)
        {
            foreach (var observer in observerList)
            {
                if (observer != obs) {
                    try {
                        observer.Notified();
                    } catch (IOException ioException) {
                        Console.Error.Write(ioException.StackTrace);
                    }
                }
            }
        }
    }
}