using InstrumentAbstraction.InstrumentInterfaces;
using Instruments.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keithley2602A
{
    public class Keithley2602ASourceMeasurementChannel:AbstractChannel, ISourceMeasurementUnit
    {
        public Keithley2602ACommandBuilder CommandSet { get; private set; }
        public Keithley2602AChannelsEnum ChannelID { get; private set; }
        public Keithley2601ASourceModeEnum ChannelSourceMode { get; set; }

        public Keithley2602ASourceMeasurementChannel(Keithley2602AChannelsEnum CurrentChannelID, Keithley2602A ParentDevice):base(CurrentChannelID,ParentDevice)
        {
            ChannelID = CurrentChannelID;
        }

        protected override void InitializeChannel()
        {
            CommandSet =((Keithley2602A)ParentDevice).CommandSet;
        }

        public void SwitchOn()
        {
            var command = CommandSet.SwitchChannelState(ChannelID, Keithley2602AChannelStatuEnum.Channel_ON);
            SendCommand(command);
        }

        public void SwitchOff()
        {
            var command = CommandSet.SwitchChannelState(ChannelID, Keithley2602AChannelStatuEnum.Channel_OFF);
            SendCommand(command);
        }

        public bool SetVoltageLimit(double Value)
        {
            var command= CommandSet.SetSourceLimit(Value, Keithley2601ALimitModeEnum.Voltage, ChannelID);
            return SendCommand(command);
        }

        public bool SetCurrentLimit(double Value)
        {
            var command = CommandSet.SetSourceLimit(Value, Keithley2601ALimitModeEnum.Current, ChannelID);
            return SendCommand(command);
        }

        public bool SetSourceVoltage(double Value)
        {
            var command = CommandSet.SetValueToChannel(Value, Keithley2601ASourceModeEnum.Voltage, ChannelID);
            return SendCommand(command);
        }

        public bool SetSourceCurrent(double Value)
        {
            var command = CommandSet.SetValueToChannel(Value, Keithley2601ASourceModeEnum.Current, ChannelID);
            return SendCommand(command);
        }

        public double MeasureVoltage(int NumberOfAverages, double TimeDelay)
        {
            var command = CommandSet.IVMeasurementQuery(Keithley2601AMeasureModeEnum.Voltage, ChannelID, NumberOfAverages, TimeDelay);
            var responce = QueryCommand(command);
            return CommandSet.IVMeasurementQueryParse(responce);
        }

        public double MeasureCurrent(int NumberOfAverages, double TimeDelay)
        {
            var command = CommandSet.IVMeasurementQuery(Keithley2601AMeasureModeEnum.Current, ChannelID, NumberOfAverages, TimeDelay);
            var responce = QueryCommand(command);
            return CommandSet.IVMeasurementQueryParse(responce);
            
        }


        private Keithley2601ASourceModeEnum GetSourceMode(SourceMode sourceMode)
        {
            switch (sourceMode)
            {
                case SourceMode.Voltage:
                    return Keithley2601ASourceModeEnum.Voltage;
                case SourceMode.Current:
                    return Keithley2601ASourceModeEnum.Current;
                default:
                    throw new ArgumentException();
            }
        }


        public double MeasureResistance(double valueThroughTheStrusture, int NumberOfAverages, double TimeDelay, SourceMode sourceMode)
        {
            Keithley2601ASourceModeEnum source =GetSourceMode(sourceMode);
            var command= CommandSet.RPMeasurementQuery(Keithley2601AMeasureModeEnum.Resistance, ChannelID, NumberOfAverages, TimeDelay,1,40, source,valueThroughTheStrusture);
            var responce = QueryCommand(command);
            return CommandSet.RPMeasurementQueryParse(responce);
        }

      

        public double MeasurePower(double valueThroughTheStrusture, int NumberOfAverages, double TimeDelay, SourceMode sourceMode)
        {
            Keithley2601ASourceModeEnum source = GetSourceMode(sourceMode);
            var command = CommandSet.RPMeasurementQuery(Keithley2601AMeasureModeEnum.Power, ChannelID, NumberOfAverages, TimeDelay, 1, 40, source, valueThroughTheStrusture);
            var responce = QueryCommand(command);
            return CommandSet.RPMeasurementQueryParse(responce);
        }


        protected override void InitChannelName(out IChannelName channelName, Enum ChannelIdentifier)
        {
            if (ChannelIdentifier.GetType() != typeof(Keithley2602AChannelsEnum))
                throw new ArgumentException("wrong enumeration type");
            channelName = new Keithley2602AChannelName((Keithley2602AChannelsEnum)ChannelIdentifier);
            
        }

        

        
    }
}
