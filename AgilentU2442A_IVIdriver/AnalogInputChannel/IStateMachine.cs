using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilentU2442A_IVIdriver
{
    internal interface IStateMachine
    {
        void Start();
        void Stop();
        void Pause();
        void Abort();


    }
}
