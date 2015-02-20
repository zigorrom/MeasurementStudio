using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;
using Devices;
namespace Keithley24XXFamily
{
    public enum Keithley24XX_MeasurementSpeed{Fast, Middle, Slow};
    public class Keithley_24XX : GPIB_Device
    {
        public Keithley_24XX(byte _PrimaryAddress, byte _SecondaryAddress, byte _BoardNumber) : base(_PrimaryAddress, _SecondaryAddress, _BoardNumber) { }
        public Keithley_24XX(string IDN, int DeviceOrder=0,byte _BoardNumber=0) : base(IDN, DeviceOrder, _BoardNumber) { }

        public override bool InitDevice()
        {
            try
            {
                this.SendCommandRequest(":TRIG:COUN 1");
                //Set 1 measurement for read
                this.SendCommandRequest(":SOUR:CLE:AUTO OFF");
                //enable auto-output off
                this.SendCommandRequest(":SOUR:FUNC VOLT");
                //enable source as Voltage
                SetCurrentAndVoltageMeasurement();
                
            }
            catch
            {
                this.isAlive = false;
            }
            return this.isAlive;
        }
        public bool setSpeed(Keithley24XX_MeasurementSpeed Speed)
        {
            var Command1 = ":SENS:CURR:NPLC";
            var Command2 = ":SENS:VOLT:NPLC";
            switch(Speed)
            {
                case Keithley24XX_MeasurementSpeed.Fast: { Command1 += " MIN"; Command2 += " MIN"; break; }
                case Keithley24XX_MeasurementSpeed.Middle: { Command1 += " DEF"; Command2 += " DEF"; break; }
                case Keithley24XX_MeasurementSpeed.Slow: { Command1 += " MAX"; Command2 += " MAX"; break; }
            }
            try
            {
                this.SendCommandRequest(Command1);
                this.SendCommandRequest(Command2);
            }
            catch { this.isAlive = false; }
            return isAlive;
        }

        public bool SetCurrentLimit(double Value)
        {
            var command = ":SENS:CURR:PROT {0}";
            StringBuilder CommandBuilder = new StringBuilder();
            try
            {
                this.SendCommandRequest(CommandBuilder.AppendFormat(command, Value).ToString());
            }
            catch
            {
                this.isAlive = false;
            }
            return this.isAlive;
        }
        public bool SetVoltageLimit(double Value)
        {
            
                var command = ":SENS:VOLT:PROT {0}";
                StringBuilder CommandBuilder = new StringBuilder();
                try
                {
                    this.SendCommandRequest(CommandBuilder.AppendFormat(command, Value).ToString());
                }
                catch
                {
                    this.isAlive = false;
                }
                return this.isAlive;
            
        }

        public bool SetCurrentAndVoltageMeasurement()
        {
            try
            {
                this.SendCommandRequest(":SENS:FUNC:CONC 1");
                //Set 1 measurement for read
                this.SendCommandRequest(":SENS:FUNC 'CURR'");
                this.SendCommandRequest(":SENS:FUNC 'VOLT'");
                

            }
            catch
            {
                this.isAlive = false;
            }
            return this.isAlive;
        }

        public bool  SetVoltageMeasurement()
        {
              try
              {
                  this.SendCommandRequest(":SENS:FUNC:CONC 0");
                  //Set 1 measurement for read
                  this.SendCommandRequest(":SENS:FUNC 'VOLT'");
              

              }
              catch
              {
                  this.isAlive = false;
              }
              return this.isAlive;
        }
        public bool Reset()
        {
            try
            {
                this.SendCommandRequest("*RST");
                //Set 1 measurement for read
            }
            catch
            {
                this.isAlive = false;
            }
            return this.isAlive;
        }
       public bool SourceVoltage(double Value)
        {
            var command = ":SOUR:VOLT {0}";
            NumberFormatInfo a = new NumberFormatInfo();
            a.NumberDecimalSeparator = ".";
            a.NumberGroupSeparator = "";
            StringBuilder CommandBuilder = new StringBuilder();
            try
            {
                this.SendCommandRequest(CommandBuilder.AppendFormat(a,command, Value).ToString());
            }
            catch
            {
                this.isAlive = false;
            }
            return this.isAlive;
        }

      public  bool SourceCurrent(double Value)
        {
            var command = ":SOUR:CURR {0}";
            StringBuilder CommandBuilder = new StringBuilder();
            try
            {
                this.SendCommandRequest(CommandBuilder.AppendFormat(command, Value).ToString());
            }
            catch
            {
                this.isAlive = false;
            }
            return this.isAlive;
        }
        public bool ShowText(string Text)
        {
            try
            {
                this.SendCommandRequest(":DISP:WIND1:TEXT:STAT ON");
                //Set 1 measurement for read  
                this.SendCommandRequest(":DISP:WIND1:TEXT:DATA '" + Text+"'");
                Thread.Sleep(1000);
                this.SendCommandRequest(":DISP:WIND1:TEXT:STAT OFF");
            }
            catch
            {
                this.isAlive = false;
            }
            return this.isAlive;
        }
     public   bool MeasureAll(out double Voltage, out double Current, out double Resistance)
        {
            string result;
            try {
               result = this.RequestQuery(":READ?");
            }
            catch {
                result = "0,0,0";
                isAlive = false; 
            }
            string[] answers = result.Split(',');
            NumberFormatInfo a = new NumberFormatInfo();
            a.NumberDecimalSeparator = ".";
            a.NumberGroupSeparator = "";
            try{
                 Voltage = Convert.ToDouble(answers[0], a);
                 Current = Convert.ToDouble(answers[1], a);
                 Resistance = Convert.ToDouble(answers[2], a);
                }

            catch{
                Voltage=0;
                Current=0;
                Resistance=0;
            }
            return isAlive;
        }
        public bool SwitchOn()
        {
            try
            {
                this.SendCommandRequest(":OUTP:STAT ON");
                //Set 1 measurement for read
            }
            catch
            {
                this.isAlive = false;
            }
            return this.isAlive;
        }
        public bool SwitchOFF()
        {
            try
            {
                this.SendCommandRequest(":OUTP:STAT OFF");
                //Set 1 measurement for read
            }
            catch
            {
                this.isAlive = false;
            }
            return this.isAlive;
        }
    }
}
