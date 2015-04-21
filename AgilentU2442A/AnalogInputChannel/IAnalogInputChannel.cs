using System;
namespace AgilentU2442A
{
    public interface IAnalogInputChannel
    {
        //void InitializePollingAnalogInput(VoltageRangeEnum VoltageRange, PolarityEnum VoltagePolarity, int AverageNumber);
        //void InitializeContinuousAnalogInput(VoltageRangeEnum VoltageRange, PolarityEnum VoltagePolarity, int AverageNumber);
        double AnalogRead();
        void StartAcquisition();
        void StopAcquisition();
    }
}
