using InstrumentAbstraction.InstrumentInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keithley2602A
{
    public class Keithley2602ASourceMeasurementChannel:ISourceMeasurementUnit
    {
        public Keithley2602ACommandBuilder CommandSet { get; private set; }
        private Keithley2602AChannelsEnum m_ChannelID;
        private Keithley2602A m_ParentDevice;


        public Keithley2602ASourceMeasurementChannel(Keithley2602AChannelsEnum CurrentChannelID, Keithley2602A ParentDevice)
        {
            m_ChannelID = CurrentChannelID;
            m_ParentDevice = ParentDevice;
            CommandSet = new Keithley2602ACommandBuilder();
        }

        public void SwitchOn()
        {
            
        }

        public void SwitchOff()
        {
            throw new NotImplementedException();
        }

        public bool SetVoltageLimit(double Value)
        {
            throw new NotImplementedException();
        }

        public bool SetCurrentLimit(double Value)
        {
            throw new NotImplementedException();
        }

        public bool SetSourceVoltage(double Value)
        {
            throw new NotImplementedException();
        }

        public bool SetSourceCurrent(double Value)
        {
            throw new NotImplementedException();
        }

        public double MeasureVoltage(int NumberOfAverages, double TimeDelay)
        {
            throw new NotImplementedException();
        }

        public double MeasureCurrent(int NumberOfAverages, double TimeDelay)
        {
            throw new NotImplementedException();
        }

        public double MeasureResistance(double valueThroughTheStrusture, int NumberOfAverages, double TimeDelay, SourceMode sourceMode)
        {
            throw new NotImplementedException();
        }

        public double MeasurePower(double valueThroughTheStrusture, int NumberOfAverages, double TimeDelay, SourceMode sourceMode)
        {
            throw new NotImplementedException();
        }

    }
}
