using System;
namespace AgilentU2442A
{
    public interface IAnalogInputChannel
    {
       // void InitializeAnalogInput();
        double AnalogRead();
        void StartAcquisition();
        void StopAcquisition();
    }
}
