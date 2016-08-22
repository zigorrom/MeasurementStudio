using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilentU2442A_IVIdriver
{
    public interface IDataRouter
    {
        IDisposable Subscribe(IAnalogInputChannel observer);
    }
}
