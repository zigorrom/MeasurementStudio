using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP35670ANamespace
{
    public enum InstrumentModes
    {
        CorrelationAnalysis,
        FFTAnalysis,
        HistogramAnalysis
    }

    public enum SwitchState
    {
        On,
        Off
    }

    public class HP35670ACommandBuilder:AbstractCommandBuilder
    {
        public HP35670ACommandBuilder()
        {
            
        }


        public string TCAPtureDELete()
        {
            return ":TCAP:DEL";
        }

        public string INSTrumentSELect(InstrumentModes Mode)
        {
            const string CommandFormat = "INST:SEL {0}";
            switch (Mode)
            {
                case InstrumentModes.CorrelationAnalysis:
                    return StringFormat(CommandFormat, "CORR");
                case InstrumentModes.FFTAnalysis:
                    return StringFormat(CommandFormat, "FFT");
                case InstrumentModes.HistogramAnalysis:
                    return StringFormat(CommandFormat, "HIST");
                default:
                    throw new ArgumentException();
            }
        }

        public string INPut(int channelName, SwitchState state)
        {
            const string CommandFormat = "INP{0} {1}";
            if (channelName < 1 || channelName > 2)
                throw new ArgumentException("Channel number");
            var s = String.Empty;
            switch (state)
            {
                case SwitchState.On:
                    s = "ON";break;
                case SwitchState.Off:
                    s = "OFF";break;
                default:
                    throw new ArgumentException();
            }
            return StringFormat(CommandFormat, channelName, s);
        }

        public enum ActiveTracesEnum
        {
            A,
            B,
            C,
            D,
            AB,
            CD,
            ABCD
        }

        public string CALCulateACTive(int channelName, ActiveTracesEnum activeTraces)
        {
            const string CommandFormat = "CALC{0}:ACT {1}";
            if (channelName < 1 || channelName > 2)
                throw new ArgumentException("Channel number");

            var s = string.Empty;
            switch (activeTraces)
            {
                case ActiveTracesEnum.A:
                    s = "A"; break;
                case ActiveTracesEnum.B:
                    s = "B"; break;
                case ActiveTracesEnum.C:
                    s = "C"; break;
                case ActiveTracesEnum.D:
                    s = "D"; break;
                case ActiveTracesEnum.AB:
                    s = "AB"; break;
                case ActiveTracesEnum.CD:
                    s = "CD"; break;
                case ActiveTracesEnum.ABCD:
                    s = "ABCD"; break;
                default:
                    throw new ArgumentException();
            }

            return StringFormat(CommandFormat, channelName, s);

        }

        public enum FormatEnum
        {
            ASCII,
            REAL
        }


        public string FORMatDATA(FormatEnum format, int number)
        {
            if (number < 3 || number > 64)
                throw new ArgumentException();

            const string CommandFormat = "FORM {0}, {1}";
            var s = String.Empty;
            switch (format)
            {
                case FormatEnum.ASCII:
                    s = "ASCII";break;
                case FormatEnum.REAL:
                    s=  "REAL";break;
                default:
                    throw new ArgumentException();
            }

            return StringFormat(CommandFormat, s, number);
        }

        public enum CMDSTR
        {

        }

        public string CALCulateFEED(int channelName, CMDSTR commandString)
        {
            const string CommandFormat = "CALC{0}:FEED {1}";
            if (channelName < 1 || channelName > 2)
                throw new ArgumentException("Channel number");

            var s = string.Empty;


            return StringFormat(CommandFormat, channelName, s);

        }

    }
}
