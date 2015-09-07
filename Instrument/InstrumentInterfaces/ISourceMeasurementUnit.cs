using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentAbstraction.InstrumentInterfaces
{
    public enum SourceMode { Voltage, Current }

    public interface ISourceMeasurementUnit
    {
        void SwitchOn();
        void SwitchOff();
        bool SetVoltageLimit(double Value);
        bool SetCurrentLimit(double Value);
        bool SetSourceVoltage(double Value);
        bool SetSourceCurrent(double Value);
        double MeasureVoltage(int NumberOfAverages, double TimeDelay);
        double MeasureCurrent(int NumberOfAverages, double TimeDelay);
        double MeasureResistance(double valueThroughTheStrusture, int NumberOfAverages, double TimeDelay, SourceMode sourceMode);
        double MeasurePower(double valueThroughTheStrusture, int NumberOfAverages, double TimeDelay, SourceMode sourceMode);
    }
}
