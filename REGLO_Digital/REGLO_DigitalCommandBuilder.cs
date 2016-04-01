using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REGLO_Digital
{
    public class REGLO_DigitalCommandBuilder:AbstractCommandBuilder
    {
        public const char CorrectInputChar = '*';
        public const char IncorrectInputChar = '#';

        public const char LF = '\n';
        public const char CR = '\r';
            
        public REGLO_DigitalCommandBuilder()
            : base()
        {

        }

        private const string CommonCommandFormat = "{0}{1}\r";

        public string StartCommand(byte PumpNumber)
        {
            return StringFormat(CommonCommandFormat, PumpNumber, 'H');
        }

        public string StopCommand(byte PumpNumber)
        {
            return StringFormat(CommonCommandFormat, PumpNumber, 'I');
        }

        public string SetRevolutionDirectionCommand(byte PumpNumber, bool Clockwise)
        {
            if (Clockwise)
                return StringFormat(CommonCommandFormat, PumpNumber, 'J');
            return StringFormat(CommonCommandFormat, PumpNumber, 'K');
        }

        public string SwitchControlPanelToManualOperationCommand(byte PumpNumber, bool SetManual)
        {
            if(SetManual)
                return StringFormat(CommonCommandFormat, PumpNumber, 'A');
            else
                return StringFormat(CommonCommandFormat, PumpNumber, 'B');
        }

        public string WriteDigitsCommand(byte PumpNumber,int number)
        {
            const string CommandFormat = "{0}{1}{2}\r";
            return StringFormat(CommandFormat, PumpNumber, 'D', number);
        }
        public string WriteTextCommand(byte PumpNumber, string text)
        {
            const string CommandFormat = "{0}{1}{2}\r";
            return StringFormat(CommandFormat, PumpNumber, "DA", text);
        }

        public enum OperatingModes
        {
            PUMP_rpm,
            PUMP_FlowRate,
            DISP_Time,
            DISP_Volume,
            PAUSE_Time,
            DISP_Time_PAUSE_Time,
            DISP_Volume_PAUSE_Time,
            Vol_dependent_dispensing_within_period,
            TOTAL
        }

        public string OperatingModeCommand(byte PumpNumber, OperatingModes mode)
        {
            var command = '\0';
            switch (mode)
            {
                case OperatingModes.PUMP_rpm: command = 'L';
                    break;
                case OperatingModes.PUMP_FlowRate: command = 'M';
                    break;
                case OperatingModes.DISP_Time: command = 'N';
                    break;
                case OperatingModes.DISP_Volume: command = 'O';
                    break;
                case OperatingModes.PAUSE_Time: command = ']';
                    break;
                case OperatingModes.DISP_Time_PAUSE_Time: command = 'P';
                    break;
                case OperatingModes.DISP_Volume_PAUSE_Time: command = 'Q';
                    break;
                case OperatingModes.Vol_dependent_dispensing_within_period: command = 'G';
                    break;
                case OperatingModes.TOTAL: command = 'R';
                    break;
                default: command = 'M';
                    break;
            }
            return StringFormat(CommonCommandFormat, PumpNumber, command);
        }

        public string ActiveQueryCommand(byte PumpNumber)
        {
            return StringFormat(CommonCommandFormat, PumpNumber, 'E');
        }

        public string PumpTypeSoftwareVersionQueryCommand(byte PumpNumber)
        {
            return StringFormat(CommonCommandFormat, PumpNumber, '#');
        }

        public string SoftwareVertionQueryCommand(byte PumpNumber)
        {
            return StringFormat(CommonCommandFormat, PumpNumber, '(');
        }

        public string PumpHeadIdentificationNumberQueryCommand(byte PumpNumber)
        {
            return StringFormat(CommonCommandFormat, PumpNumber, ')');
        }
        
        public string SetPumpHeadIdentificationNumberCommand(byte PumpNumber, string ID)
        {
            return StringFormat("{0}{1}{2}\r", PumpNumber, ')',ID);
        }



    }
}
