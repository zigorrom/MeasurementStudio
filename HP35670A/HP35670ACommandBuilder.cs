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

    public enum FormatEnum
    {
        ASCII,
        REAL
    }

    public enum CMDSTR
    {
        XFR_POW
    }

    public enum VoltageUnits
    {
        V,
        V2,
        VdivRTHZ,
        V2divHZ,
        V2SdivHZ
    }

    public enum CalibrationEnum
    {
        ON,
        OFF,
        ONCE
    }

    public enum FrequencyResolutionEnum : int
    {
        r100 = 100,
        r200 = 200,
        r400 = 400,
        r800 = 800,
        r1600 = 1600
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


        public string CALCulateFEED(int channelName, CMDSTR commandString)
        {
            const string CommandFormat = "CALC{0}:FEED {1}";
            if (channelName < 1 || channelName > 2)
                throw new ArgumentException("Channel number");

            var s = string.Empty;
            switch (commandString)
            {
                case CMDSTR.XFR_POW:
                    s = "'XFR:POW 1'";break;
                default:
                    throw new ArgumentException();
            }

            return StringFormat(CommandFormat, channelName, s);

        }

       

        public string CALCulateUNITVOLTage(int channelName, VoltageUnits units)
        {
            const string CommandFormat = "CALC{0}:UNIT:VOLT {1}";
            if (channelName < 1 || channelName > 2)
                throw new ArgumentException("Channel number");

            var s = string.Empty;

            switch (units)
            {
                case VoltageUnits.V:
                  s = "'V'";  break;
                case VoltageUnits.V2:
                     s = "'V3'";  break;
                case VoltageUnits.VdivRTHZ:
                     s = "'V/RTHZ'";  break;
                case VoltageUnits.V2divHZ:
                     s = "'V2/HZ'";  break;
                case VoltageUnits.V2SdivHZ:
                     s = "'V2S/HZ'";  break;
                default:
                    throw new ArgumentException();
            }

            return StringFormat(CommandFormat, channelName, s);
        }

       
      

        public string CALibrationAUTO(CalibrationEnum cal)
        {
            const string CommandFormat = "CAL:AUTO {0}";
            
            var s = String.Empty;
            switch (cal)
            {
                case CalibrationEnum.ON:
                    s = "ON"; break;
                case CalibrationEnum.OFF:
                    s = "OFF"; break;
                case CalibrationEnum.ONCE:
                    s = "ONCE"; break;
                default:
                    throw new ArgumentException();
            }
            return StringFormat(CommandFormat, s);
        }

       
        public string FREquencyRESolution(FrequencyResolutionEnum resolution)
        {
            const string CommandFormat = "FREQ:RES {0}";
            var res = (int)resolution;
            return StringFormat(CommandFormat, res);
        }


        public string SOURceVOLTageOFFSet(double val)
        {
            if (val < -10 || val > 10)
                throw new ArgumentException();

            return StringFormat("SOUR:VOLT:OFFS {0}", val);
        }

        public string ClearStatus()
        {
            return "*CLS";
        }


        public string CalibrateQuery()
        {
            return "*CAL?";
        }

        public bool CalibrateQueryParse(string responce)
        {
            var a = StringToInt(responce);
            if (a > 0)
                throw new Exception();
            return true;
        }


        public string AVERage(SwitchState state)
        {
            const string CommandFormat = "AVER {0}";
            switch (state)
            {
                case SwitchState.On:
                    return StringFormat(CommandFormat, "ON");
                case SwitchState.Off:
                    return StringFormat(CommandFormat, "OFF");
                default:
                    throw new ArgumentException();
            }
        }

        public string AVERageIRESultRATE(int number)
        {
            if (number < 1 || number > 9999999)
                throw new ArgumentException();
            return StringFormat("AVERAGE:IRES:RATE {0}", number);
        }

        public string AVERageCOUNT(int number)
        {
            if (number < 1 || number > 9999999)
                throw new ArgumentException();
            return StringFormat("AVER:COUN {0}", number);
        }

        public string REJectSTATe(SwitchState state)
        {
            const string CommandFormat = "SENS:REJ:STAT {0}";
            switch (state)
            {
                case SwitchState.On:
                    return StringFormat(CommandFormat, "ON");
                case SwitchState.Off:
                    return StringFormat(CommandFormat, "OFF");
                default:
                    throw new ArgumentException();
            }
        }


        public string FREQuencySTARt(float startFreq)
        {
            if (startFreq < 0.0 || startFreq > 114999.9023)
                throw new ArgumentException();
            return StringFormat("SENS:FREQ:STAR {0}", startFreq);
        }

        public string FREQuencySTOP(float stopFreq)
        {
            if (stopFreq < 0.03125 || stopFreq > 115000.0)
                throw new ArgumentException();
            return StringFormat("SENS:FREQ:STOP {0}", stopFreq);
        }

        public string ABORT()
        {
            return "ABORT";
        }

        public string INIT()
        {
            return "INIT";
        }

        public string OUTPutSTATus(SwitchState state)
        {
            const string CommandFormat = "OUTPUT:STAT {0}";
            switch (state)
            {
                case SwitchState.On:
                    return StringFormat(CommandFormat, "ON");
                case SwitchState.Off:
                    return StringFormat(CommandFormat, "OFF");
                default:
                    throw new ArgumentException();
            }
        }

        public string DataQuery(int channelName)
        {
            const string CommandFormat = "CALC{0}:DATA?";
            if (channelName < 1 || channelName > 2)
                throw new ArgumentException("Channel number");
            return StringFormat(CommandFormat, channelName);
        }




        public string CALCulateDATAHEADerPOINtsQuery()
        {
            return "CALC1:DATA:HEAD:POIN?";
        }

        public string WAItQuery()
        {
            return "*WAI";
        }
    }
}
