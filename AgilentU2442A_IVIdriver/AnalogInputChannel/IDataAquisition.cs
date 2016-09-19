using System;
namespace AgilentU2442A_IVIdriver
{
    public interface IDataAquisition
    {
        int SampleRate { get; set; }
        int SamplesPerShot { get; set; }
        void StartAcquisition();
        void StopAcquisition();
    }
}
