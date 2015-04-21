using System;
namespace AgilentU2442A
{
    public interface IAnalogInputChannel
    {
        void InitializeAnalogInput(VoltageRangeEnum VoltageRange, PolarityEnum VoltagePolarity, int AverageNumber);
        double AnalogRead();
        void StartAcquisition();
        void StopAcquisition();
    }
}
