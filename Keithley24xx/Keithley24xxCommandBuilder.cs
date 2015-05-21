using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keithley24xx
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
            switch (function)
            {
                case FunctionEnum.CURRent:
                    return StringFormat(CommandFormat, ":CURR");
                case FunctionEnum.VOLTage:
                    return StringFormat(CommandFormat, ":VOLT");
                case FunctionEnum.RESistance:
                    return StringFormat(CommandFormat, ":RES");
                case FunctionEnum.None:
                default:
                    return StringFormat(CommandFormat, "");
            }
        }

        public string SourceVoltage(double Value)
        {
            const string CommandFormat = ":SOUR:VOLT {0}";
            return StringFormat(CommandFormat, Value);
        }


    }
}
