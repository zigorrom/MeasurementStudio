using System;
namespace AgilentU2442A
{
    interface IAnalogInputChannel
    {
        void InitializeAnalogInput();
        double AnalogRead();
        void StartAcquisition();
        void StopAcquisition();
    }
}
