using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;
using Instruments;
using InstrumentAbstraction.InstrumentInterfaces;
using System.ComponentModel.Composition;



namespace Keithley24xxNamespace
{

    [Export(typeof(IInstrument))]
    [ExportMetadata("InstrumentMetadata",typeof(ISourceMeasurementUnit))]
    [InstrumentAttribute("KEITHLEY", "24")]//24 - BECAUSE 2400,2430 FITS
    public class Keithley24xx : AbstractMessageBasedInstrument, ISourceMeasurementUnit//, ISourceMeasurementUnit
    {
        public Keithley24xx(string Name, string Alias, string ResourceName)
            : base(Name, Alias, ResourceName)
        {
            //ke2400 ke = new ke2400("", false, false);

            // ke2400 ke = new ke2400(ResourceName, true, true);
            
        }

        public Keithley24xx() : base("", "", "") { }

        private Keithley24xxCommandBuilder m_CommandSet;
        //public override AbstractCommandBuilder CommandSet
        //{
        //    get { return m_CommandSet; }
        //}


        public override bool InitializeDevice()
        {
            if (base.InitializeDevice())
            {
                m_CommandSet = new Keithley24xxCommandBuilder();
                SendCommand(m_CommandSet.TrigerCount(1));
                //set 1 measurement for read
                SendCommand(m_CommandSet.SourceCleAuto(SourceCleAutoEnum.Off)); //    ":SOUR:CLE:AUTO OFF");
                // enable auto-output off
                SendCommand(m_CommandSet.SourceFunction(SourceMode.Voltage));    //":SOUR:FUNC VOLT");
                //enable source as Voltage

                ////SET CURRENT AND VOLTAGE MEASUREMENT
                //SendCommand(":SENS:FUNC:CONC 1");
                //SendCommand(":SENS:FUNC 'CURR'");
                //SendCommand(":SENS:FUNC 'VOLT'");
                SetCurrentAndVoltageMeasurement();

            }
            return false;

        }


        public void SwitchOn()
        {
            SendCommand(":OUTP:STAT ON");
        }

        public void SwitchOff()
        {
            SendCommand(":OUTP:STAT OFF");
        }

        public bool SetSpeed(MeasurementSpeed Speed)
        {

            if (SendCommand(m_CommandSet.SetSpeed(FunctionEnum.CURRent, Speed)) && SendCommand(m_CommandSet.SetSpeed(FunctionEnum.VOLTage, Speed)))//String.Format(CommandFormat, "CURR", SpeedIdentifier)) && SendCommand(String.Format(CommandFormat, "VOLT", SpeedIdentifier)))
                return true;
            return false;
        }

        public bool SetCurrentLimit(double Value)
        {
            if (SendCommand(m_CommandSet.SetCurrentLimit(Value))) //String.Format(CommandFormat, Value)))
                return true;
            return false;
        }

        public bool SetVoltageLimit(double Value)
        {
            //var CommandFormat = ":SENS:VOLT:PROT {0}";
            if (SendCommand(m_CommandSet.SetVoltageLimit(Value)))//String.Format(CommandFormat, Value)))
                return true;
            return false;
        }

        public bool SetCurrentAndVoltageMeasurement()
        {
            if (SendCommand(m_CommandSet.SenseFunctionConcurrent(SenseFuncConcurrentEnum.On)))   //":SENS:FUNC:CONC 1"))
                if (SendCommand(m_CommandSet.SetSenseFunction(FunctionEnum.CURRent)))  //":SENS:FUNC 'CURR'"))
                    if (SendCommand(m_CommandSet.SetSenseFunction(FunctionEnum.VOLTage))) //":SENS:FUNC 'VOLT'"))
                        return true;
            return false;
        }

        public bool SetVoltageMeasurement()
        {
            if (SendCommand(m_CommandSet.SenseFunctionConcurrent(SenseFuncConcurrentEnum.Off)))//":SENS:FUNC:CONC 0"))
                if (SendCommand(m_CommandSet.SetSenseFunction(FunctionEnum.VOLTage)))
                    return true;
            return false;
        }
        public override void Reset()
        {
            SendCommand("*RST");
        }


        public bool SetSourceVoltage(double Value)
        {
            //var CommandFormat = ":SOUR:VOLT {0}";
            //var numForm = new NumberFormatInfo() { NumberDecimalSeparator = ".", NumberGroupSeparator = "" };
            if (SendCommand(m_CommandSet.setSourceVoltage(Value)))//String.Format(numForm, CommandFormat, Value)))
                return true;
            return false;
        }


        //public bool SourceCurrent(double Value)
        //{
        //    var CommandFormat = ":SOUR:CURR {0}";
        //    var numForm = new NumberFormatInfo() { NumberDecimalSeparator = ".", NumberGroupSeparator = "" };
        //    if (SendCommand(String.Format(numForm, CommandFormat, Value)))
        //        return true;
        //    return false;
        //}

        public bool SetSourceCurrent(double Value)
        {
            //var CommandFormat = ":SOUR:CURR {0}";
            //var numForm = new NumberFormatInfo() { NumberDecimalSeparator = ".", NumberGroupSeparator = "" };
            if (SendCommand(m_CommandSet.SetSourceCurrent(Value)))//String.Format(numForm, CommandFormat, Value)))
                return true;
            return false;
        }

        private bool ShowText(string text)
        {
            //var CommandFormat = ":DISP:WIND1:TEXT:STAT {0}"; 
            if (SendCommand(m_CommandSet.TextStatus(TextStatusEnum.On))) //String.Format(CommandFormat,"ON")))
                if (SendCommand(m_CommandSet.ShowText(text)))//String.Format(":DISP:WIND1:TEXT:DATA '{0}'",text)))
                {
                    Thread.Sleep(500);
                    if (SendCommand(m_CommandSet.TextStatus(TextStatusEnum.Off)))//String.Format(CommandFormat, "OFF")))
                        return true;
                }
            return false;
        }

        public bool MeasureAll(out double Voltage, out double Current, out double Resistance)
        {
            Voltage = 0;
            Current = 0;
            Resistance = 0;
            var result = Query(m_CommandSet.READQuery());
            if (String.IsNullOrEmpty(result))
                return false;
            string[] strValues = result.Split(',');
            Voltage = m_CommandSet.StringToDouble(strValues[0]);
            Current = m_CommandSet.StringToDouble(strValues[1]);
            Resistance = m_CommandSet.StringToDouble(strValues[2]);
            return true;
        }

        public double MeasureVoltage(int NumberOfAverages, double TimeDelay)
        {
            double Voltage, Current, Resistance;
            MeasureAll(out Voltage, out Current, out Resistance);
            return Voltage;

        }

        public double MeasureCurrent(int NumberOfAverages, double TimeDelay)
        {
            double Voltage, Current, Resistance;
            MeasureAll(out Voltage, out Current, out Resistance);
            return Current;
        }

        public double MeasureResistance(double valueThroughTheStrusture, int NumberOfAverages, double TimeDelay, SourceMode sourceMode)
        {
            double Voltage, Current, Resistance;
            MeasureAll(out Voltage, out Current, out Resistance);
            return Resistance;
        }

        public double MeasurePower(double valueThroughTheStrusture, int NumberOfAverages, double TimeDelay, SourceMode sourceMode)
        {
            throw new NotImplementedException();
        }



        public override void DetectInstrument(object data)
        {
            throw new NotImplementedException();
        }









    }
}
