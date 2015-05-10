using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;
using Instruments;
using Ke2400DotNetWrapper;

namespace Keithley24xx
{
    [InstrumentAttribute("KEITHLEY","24")]//24 - BECAUSE 2400,2430 FITS
    public class Keithley24xx:AbstractMessageBasedInstrument//, ISourceMeasurementUnit
    {
        public Keithley24xx(string Name,string Alias, string ResourceName):base(Name,Alias,ResourceName)
        {
           // ke2400 ke = new ke2400(ResourceName, true, true);
            CommandSet = new Keithley24xxCommandBuilder();
        }

        private Keithley24xxCommandBuilder CommandSet;

        public override bool InitializeDevice()
        {
            if(base.InitializeDevice())
            {
                SendCommand(":TRIG:COUN 1");
                //set 1 measurement for read
                SendCommand(":SOUR:CLE:AUTO OFF");
                // enable auto-output off
                SendCommand(":SOUR:FUNC VOLT");
                //enable source as Voltage

                ////SET CURRENT AND VOLTAGE MEASUREMENT
                //SendCommand(":SENS:FUNC:CONC 1");
                //SendCommand(":SENS:FUNC 'CURR'");
                //SendCommand(":SENS:FUNC 'VOLT'");
                SetCurrentAndVoltageMeasurement();

            }
            return false;

        }

        

        public bool SetSpeed(MeasurementSpeed Speed)
        {
            var CommandFormat = ":SENS:{0}:NPLC {1}";
            var SpeedIdentifier = "";
            switch (Speed)
            {
                case MeasurementSpeed.Fast:
                    SpeedIdentifier = "MIN";
                    break;
                case MeasurementSpeed.Middle:
                    SpeedIdentifier = "DEF";
                    break;
                case MeasurementSpeed.Slow:
                    SpeedIdentifier = "MAX";
                    break;
            }
            if (SendCommand(String.Format(CommandFormat, "CURR", SpeedIdentifier)) && SendCommand(String.Format(CommandFormat, "VOLT", SpeedIdentifier)))
                return true;
            return false;
        }

        public bool SetCurrentLimit(double Value)
        {
            var CommandFormat = ":SENS:CURR:PROT {0}";
            if (SendCommand(String.Format(CommandFormat, Value)))
                return true;
            return false;
        }

        public bool SetVoltageLimit(double Value)
        {
            var CommandFormat = ":SENS:VOLT:PROT {0}";
            if (SendCommand(String.Format(CommandFormat, Value)))
                return true;
            return false;
        }

        public bool SetCurrentAndVoltageMeasurement()
        {
            if (SendCommand(":SENS:FUNC:CONC 1"))
                if (SendCommand(":SENS:FUNC 'CURR'"))
                    if (SendCommand(":SENS:FUNC 'VOLT'"))
                        return true;
            return false;
        }

        public bool SetVoltageMeasurement()
        {
            if (SendCommand(":SENS:FUNC:CONC 0"))
                if (SendCommand(":SENS:FUNC 'VOLT'"))
                    return true;
            return false;
        }
        public override void Reset()
        {
            SendCommand("*RST");
        }
        //public bool Reset()
        //{
        //    if (SendCommand("*RST"))
        //        return true;
        //    return false;
        //}

        public bool SourceVoltage(double Value)
        {
            var CommandFormat = ":SOUR:VOLT {0}";
            var numForm = new NumberFormatInfo() { NumberDecimalSeparator = ".", NumberGroupSeparator = "" };
            if (SendCommand(String.Format(numForm, CommandFormat, Value)))
                return true;
            return false;
        }

        public bool SourceCurrent(double Value)
        {
            var CommandFormat = ":SOUR:CURR {0}";
            var numForm = new NumberFormatInfo() { NumberDecimalSeparator = ".", NumberGroupSeparator = "" };
            if (SendCommand(String.Format(numForm, CommandFormat, Value)))
                return true;
            return false;
        }

        public bool ShowText(string text)
        {
            var CommandFormat = ":DISP:WIND1:TEXT:STAT {0}"; 
            if(SendCommand(String.Format(CommandFormat,"ON")))
                if(SendCommand(String.Format(":DISP:WIND1:TEXT:DATA '{0}'",text)))
                {
                    Thread.Sleep(500);
                    if (SendCommand(String.Format(CommandFormat, "OFF")))
                        return true;
                }
            return false;
        }

        public bool MeasureAll(out double Voltage, out double Current, out double Resistance)
        {
            Voltage = 0;
            Current = 0;
            Resistance = 0;
            var result = Query(":READ?");
            if (String.IsNullOrEmpty(result))
                return false;
            string[] strValues = result.Split(',');
            Voltage = CommandSet.StringToDouble(strValues[0]);
            Current = CommandSet.StringToDouble(strValues[1]);
            Resistance = CommandSet.StringToDouble(strValues[2]);
            return true;
        }

        public bool SwitchOn()
        {
            if (SendCommand(":OUTP:STAT ON"))
                return true;
            return false;
        }
        public bool SwitchOff()
        {
            if (SendCommand(":OUTP:STAT OFF"))
                return true;
            return false;
        }

        public override void DetectInstrument()
        {
            throw new NotImplementedException();
        }



       
    }
}
