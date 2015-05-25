using System;
namespace AgilentU2442A
{
    public interface IAnalogInputChannel
    {
        event EventHandler DataSetReady;
        double AnalogRead();
        double AnalogRead(int NumberOfAverages);
        string SingleShotAquicition();
        void SingleShotAquicition(out double[] data);
        void StartAcquisition();
        void StopAcquisition();
    }
}
