using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentViewer
{
    public abstract class ObservableExperiment<T>:IObservable<T>
    {
        private class Unsubscriber:IDisposable
        {
            private List<IObserver<T>> _observers;
            private IObserver<T> _observer;
            public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
            {
                _observer = observer;
                _observers = observers;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public ObservableExperiment()
        {
            _observers = new List<IObserver<T>>();
        }

        private List<IObserver<T>> _observers;

        
        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        public void Notify(T obj)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(obj);
            }
        }
    }
}
