using System;
namespace AgilentU2442A
{
    interface IAnalogInputChannel
    {
        double AnalogRead();
        void StartAcquisition();
        void StopAcquisition();
    }
}
