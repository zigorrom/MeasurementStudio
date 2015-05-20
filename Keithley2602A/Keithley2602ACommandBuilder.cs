using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keithley2602A
{
    public class Keithley2602ACommandBuilder:AbstractCommandBuilder
    {
        public string BeeperEnable()
        {
            return "beeper.enable = 1 ";
        }

        private const string NoSuchChannelExceptionString = "No such channel";
        private const char ChannelASign = 'a';
        private const char ChannelBSign = 'b';
        private const char CurrentSign = 'i';
        private const char VoltageSign = 'v';
        private const string SMUASign = "SMUA";
        private const string SMUBSign = "SMUB";

        public double DoubleFromString(string text)
        {
            var val = 0.0;
            if (double.TryParse(text, System.Globalization.NumberStyles.Float, m_currentInfo, out val))
                return val;
            throw new ArgumentException();
        }

        private char GetNameFromID(Keithley2602AChannelsEnum ChannelID)
        {
            switch (ChannelID)
            {
                case Keithley2602AChannelsEnum.ChannelA:
                    return ChannelASign;
                case Keithley2602AChannelsEnum.ChannelB:
                    return ChannelBSign;
                default:
                    throw new ArgumentException(NoSuchChannelExceptionString);
            }
        }
        private string GetSMUNameFromID(Keithley2602AChannelsEnum ChannelID)
        {
            switch (ChannelID)
            {
                case Keithley2602AChannelsEnum.ChannelA:
                    return SMUASign;
                case Keithley2602AChannelsEnum.ChannelB:
                    return SMUBSign;
                default:
                    throw new ArgumentException(NoSuchChannelExceptionString);
            }
        }
      

        internal string SwitchChannelState(Keithley2602AChannelsEnum ChannelID, Keithley2602AChannelStatuEnum State)
        {
            
            const string CommandFormat = 
@"beeper.beep(0.15, 2400) 
smu{0}.source.output = smu{0}.{1} ";

            var name = GetNameFromID(ChannelID);

            switch (State)
            {
                case Keithley2602AChannelStatuEnum.Channel_ON:
                    return StringFormat(CommandFormat, name, "OUTPUT_ON");
                case Keithley2602AChannelStatuEnum.Channel_OFF:
                    return StringFormat(CommandFormat, name, "OUTPUT_OFF");
                default:
                    throw new ArgumentException(NoSuchChannelExceptionString);
            }
        }


        internal string SetSourceLimit(double Value, Keithley2601ALimitModeEnum keithley2601ALimitModeEnum, Keithley2602AChannelsEnum ChannelID)
        {
            const string CommandFormat = "smu{0}.source.limit{1} = {2} ";

            var name = GetNameFromID(ChannelID);
            
            switch (keithley2601ALimitModeEnum)
            {
                case Keithley2601ALimitModeEnum.Voltage:
                    return StringFormat(CommandFormat, name, VoltageSign, Value);
                case Keithley2601ALimitModeEnum.Current:
                    return StringFormat(CommandFormat, name, CurrentSign, Value);
                default:
                    throw new ArgumentException("Wrong limit mode");
            }
        }


        internal string SetValueToChannel(double Value, Keithley2601ASourceModeEnum keithley2601ASourceModeEnum, Keithley2602AChannelsEnum ChannelID)
        {
            const string CommandFormat = "smu{0}.source.level{1} = {2} ";

            var name = GetNameFromID(ChannelID);
        
            switch (keithley2601ASourceModeEnum)
            {
                case Keithley2601ASourceModeEnum.Voltage:
                    return StringFormat(CommandFormat, name, VoltageSign, Value);
                case Keithley2601ASourceModeEnum.Current:
                    return StringFormat(CommandFormat, name, CurrentSign, Value);
                default:
                    throw new ArgumentException("Wrong source mode");
            }
            
        }

        internal string IVMeasurementQuery(Keithley2601AMeasureModeEnum keithley2601AMeasureModeEnum, Keithley2602AChannelsEnum ChannelID, int AveragingNumber, double TimeDelay)
        {
            const string IVCommandFormat =
                "loadscript MeasureValueInChannel\n" +
                     "smu{0}.measure.autorange{1} = smu{0}.AUTORANGE_ON\n" +
                     "display.screen = display.{4}\n" +
                     "display.smu{0}.measure.func = display.{5}\n" +
                     "trigger.clear()\n" +
                     "result = 0.0\n" +
                     "for parameterMeasure = 1, {2} do\n" +
                     "trigger.wait({3})\n" +
                     "result = result + smu{0}.measure.{1}()\n" +
                     "end\n" +
                     "result = result / ({2} - 1)\n" +
                     "print (result)\n" +
                     "endscript\n" +
                     "MeasureValueInChanel()\n";
              var name = GetNameFromID(ChannelID);
            var smuName = GetSMUNameFromID(ChannelID);
            switch (keithley2601AMeasureModeEnum)
            {
                case Keithley2601AMeasureModeEnum.Voltage:
                    return StringFormat(IVCommandFormat, name, VoltageSign, AveragingNumber, TimeDelay, smuName, "MEASURE_DCVOLTS");
                case Keithley2601AMeasureModeEnum.Current:
                    return StringFormat(IVCommandFormat, name, CurrentSign, AveragingNumber, TimeDelay, smuName, "MEASURE_DCAMPS");
                default:
                    throw new NotImplementedException();
            }
        }

        public double IVMeasurementQueryParse(string responce)
        {
            return DoubleFromString(responce);
            
        }
        public double RPMeasurementQueryParse(string responce)
        {
            return DoubleFromString(responce);

        }
        internal string RPMeasurementQuery(Keithley2601AMeasureModeEnum keithley2601AMeasureModeEnum, Keithley2602AChannelsEnum ChannelID, int AveragingNumber, double TimeDelay, double limitI, double limitV, Keithley2601ASourceModeEnum PowerAndResistanceSourceMode ,double PowerAndResistanceSourceValue)
        {
            const string RCommandFormat =
                "loadscript MeasureResistanceInChannel\n" +
                    "smu{0}.source.func = smu{0}.{1}\n" +
                    "smu{0}.source.autorange{2} = smu{0}.AUTORANGE_ON\n" +
                    "smu{0}.source.level{2} = {3}\n" +
                    "smu{0}.source.limit{4} = {5}\n" +
                    "smu{0}.measure.autorange{4} = smu{0}.AUTORANGE_ON\n" +
                    "display.screen = display.{6}\n" +
                    "display.smu{0}.measure.func = display.{7}\n" +
                    "trigger.clear()\n" +
                    "result = 0.0\n" +
                    "for parameterMeasure = 1, {8} do\n" +
                    "trigger.wait({9})\n" +
                    "result = result + smu{0}.measure.r()\n" +
                    "end\n" +
                    "result = result / {8}\n" +
                    "print (result)\n" +
                    "endscript\n" +
                    "MeasureResistanceInChannel()\n";

            const string PCommandFormat =
                "loadscript MeasurePowerInChannel\n" +
                    "smu{0}.measure.autorange{1} = smu{0}.AUTORANGE_ON\n" +
                    "display.screen = display.{2}\n" +
                    "display.smu{0}.measure.func = display.{3}\n" +
                    "trigger.clear()\n" +
                    "result = 0.0\n" +
                    "for parameterMeasure = 1, {4} do\n" +
                    "trigger.wait({5})\n" +
                    "result = result + smu{0}.measure.p()\n" +
                    "end\n" +
                    "result = result / ({4} - 1)\n" +
                    "print (result)\n" +
                    "endscript\n" +
                    "MeasurePowerInChannel()\n";

            var name = GetNameFromID(ChannelID);
            var smuName = GetSMUNameFromID(ChannelID);
            var source = GetRPsourceString(PowerAndResistanceSourceMode);
            var sourceUnit = GetRPsourceUnitString(PowerAndResistanceSourceMode);
            var measureUnit = GetRPmeasureUnitString(PowerAndResistanceSourceMode);
            var limitVal = 0.0;
            switch (PowerAndResistanceSourceMode)
            {
                case Keithley2601ASourceModeEnum.Voltage:
                    limitVal = limitI; break;
                case Keithley2601ASourceModeEnum.Current:
                    limitVal = limitV;break;
                default:
                    break;
            }

            switch (keithley2601AMeasureModeEnum)
            {
                case Keithley2601AMeasureModeEnum.Resistance:
                    return StringFormat(RCommandFormat, name, source, sourceUnit, PowerAndResistanceSourceValue, measureUnit, limitVal, smuName, "MEASURE_OHMS", AveragingNumber, TimeDelay);
                case Keithley2601AMeasureModeEnum.Power:
                    return StringFormat(PCommandFormat, name, measureUnit, smuName, "MEASURE_WATTS", AveragingNumber, TimeDelay);
                default:
                    throw new NotImplementedException();
            }
        }

        private object GetRPmeasureUnitString(Keithley2601ASourceModeEnum PowerAndResistanceSourceMode)
        {
            switch (PowerAndResistanceSourceMode)
            {
                case Keithley2601ASourceModeEnum.Voltage:
                    return CurrentSign;
                case Keithley2601ASourceModeEnum.Current:
                    return VoltageSign;
                default:
                    throw new ArgumentException("No such mode");
            }
        }

        private char GetRPsourceUnitString(Keithley2601ASourceModeEnum PowerAndResistanceSourceMode)
        {
            switch (PowerAndResistanceSourceMode)
            {
                case Keithley2601ASourceModeEnum.Voltage:
                    return VoltageSign;
                case Keithley2601ASourceModeEnum.Current:
                    return CurrentSign;
                default:
                    throw new ArgumentException("No such mode");
            }
        }
            
        private string GetRPsourceString(Keithley2601ASourceModeEnum sourceMode)
        {
            switch(sourceMode)
            {
            case Keithley2601ASourceModeEnum.Voltage:
                    return "OUTPUT_DCVOLTS";
                case Keithley2601ASourceModeEnum.Current:
                    return "OUTPUT_DCAMPS";
                default:
                    throw new ArgumentException("No such source mode");
            }
        }
    }


}
