using InstrumentAbstraction.InstrumentInterfaces;
using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keithley24xxNamespace
{
    public class Keithley24xxCommandBuilder:AbstractCommandBuilder
    {
        public Keithley24xxCommandBuilder() : base() { }

        public string CONFigure(FunctionEnum function)
        {
            const string CommandFormat = ":CONF:{0}\n";
            var func = String.Empty;
            switch (function)
            {
                case FunctionEnum.CURRent:
                    func = "CURR";
                    break;
                case FunctionEnum.VOLTage:
                    func = "VOLT";
                    break;
                case FunctionEnum.RESistance:
                    func = "RES";
                    break;
            }
            return StringFormat(CommandFormat, func);
        }

        public string CONFigureQuery()
        {
            return ":CONF?\n";
        }

        public string FETChQuery()
        {
            return ":FETCh?\n";
        }

        public string DATAQuery()
        {
            return ":DATA?\n";
        }

        public string READQuery()
        {
            return ":READ?\n";
        }

        public string MEASure(FunctionEnum function)
        {
            const string CommandFormat = ":MEAS{0}?\n";
            var func = GetFunctuionAsString(function);
            return StringFormat(CommandFormat, func);
        }


        internal string TrigerCount(int count)
        {
            string CommandFormat = ":TRIG:COUN {0}";
            return StringFormat(CommandFormat, count);
        }

        internal string SourceCleAuto(SourceCleAutoEnum sourceCleAutoEnum)
        {
            const string CommandFormat = ":SOUR:CLE:AUTO {0}";
            switch (sourceCleAutoEnum)
            {
                case SourceCleAutoEnum.On:
                    return StringFormat(CommandFormat, "ON");
                case SourceCleAutoEnum.Off:
                    return StringFormat(CommandFormat, "OFF");
                default:
                    throw new ArgumentException();
            }
        }

        internal string SourceFunction(InstrumentAbstraction.InstrumentInterfaces.SourceMode sourceMode)
        {
            const string CommandFormat = ":SOUR:FUNC {0}";
            switch (sourceMode)
            {
                case SourceMode.Voltage:
                    return StringFormat(CommandFormat, "VOLT");
                case SourceMode.Current:
                    return StringFormat(CommandFormat, "CURR");
                default:
                    throw new ArgumentException();
            }
        }

        internal string SenseFunctionConcurrent(SenseFuncConcurrentEnum senseFuncConcurrentEnum)
        {
            const string CommandFormat = ":SENS:FUNC:CONC {0}";
            switch (senseFuncConcurrentEnum)
            {
                case SenseFuncConcurrentEnum.On:
                    return StringFormat(CommandFormat, "1");
                case SenseFuncConcurrentEnum.Off:
                    return StringFormat(CommandFormat, "0");
                default:
                    throw new ArgumentException();
            }
        }

        internal string SetSenseFunction(FunctionEnum senseFunction)
        {
            const string CommandFormat = ":SENS:FUNC '{0}'";
            var func = GetFunctuionAsString(senseFunction);
            return StringFormat(CommandFormat, func);
        }

        private string GetFunctuionAsString(FunctionEnum senseFunction)
        {
            switch (senseFunction)
            {
                case FunctionEnum.None:
                    return "";
                case FunctionEnum.CURRent:
                    return "CURR";
                case FunctionEnum.VOLTage:
                    return "VOLT";
                case FunctionEnum.RESistance:
                    return "RES";
                default:
                    throw new ArgumentException();
            }
        }

        internal string SetSpeed(FunctionEnum function, MeasurementSpeed speed)
        {
            const string CommandFormat = ":SENS:{0}:NPLC {1}";
            var func = GetFunctuionAsString(function);
            switch (speed)
            {
                case MeasurementSpeed.Fast:
                    return StringFormat(CommandFormat, func, "MIN");
                case MeasurementSpeed.Middle:
                    return StringFormat(CommandFormat, func, "DEF");
                case MeasurementSpeed.Slow:
                    return StringFormat(CommandFormat, func, "MAX");
                default:
                    throw new ArgumentException();
            }
        }



        internal string SetCurrentLimit(double Value)
        {
            const string CommandFormat = ":SENS:CURR:PROT {0}";
            return StringFormat(CommandFormat, Value);
        }

        internal string SetVoltageLimit(double Value)
        {
            const string CommandFormat = ":SENS:VOLT:PROT {0}";
            return StringFormat(CommandFormat, Value);
        }

        internal string setSourceVoltage(double Value)
        {
            const string CommandFormat = ":SOUR:VOLT {0}";
            return StringFormat(CommandFormat, Value);
        }



        internal string SetSourceCurrent(double Value)
        {
            const string CommandFormat = ":SOUR:CURR {0}";
            return StringFormat(CommandFormat, Value);
        }

        internal string TextStatus(TextStatusEnum textStatusEnum)
        {
            const string CommandFormat = ":DISP:WIND1:TEXT:STAT {0}";
            switch (textStatusEnum)
            {
                case TextStatusEnum.On:
                    return StringFormat(CommandFormat, "ON");
                case TextStatusEnum.Off:
                    return StringFormat(CommandFormat, "OFF");
                default:
                    throw new ArgumentException();
            }
        }

        internal string ShowText(string text)
        {
            const string CommandFormat = ":DISP:WIND1:TEXT:DATA '{0}'";
            return StringFormat(CommandFormat, text);
        }
    }
}
