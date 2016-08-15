using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A_IVIdriver
{
    internal class AnalogInputDataRouter:IObservable<double[]>
    {
        public IDisposable Subscribe(IObserver<double[]> observer)
        {
            throw new NotImplementedException();
        }

    }
}
