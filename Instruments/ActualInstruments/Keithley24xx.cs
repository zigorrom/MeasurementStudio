using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments
{
    [InstrumentAttribute("KEITHLEY","24")]//24 - BECAUSE 2400,2430 FITS
    public class Keithley24xx:AbstractMessageBasedInstrument, ISourceMeasurementUnit
    {
        public Keithley24xx(string Name,string Alias, string ResourceName):base(Name,Alias,ResourceName)
        {

        }

        public override bool InitializeDevice()
        {
            if(base.InitializeDevice())
            {
                SendCommand(":TRIG:COUN 1");
                SendCommand(":SOUR:CLE:AUTO OFF");
                SendCommand(":SOUR:FUNC VOLT");


            }
            return false;

        }
        public bool TryReadVoltage(out double Voltage)
        {
            throw new NotImplementedException();
        }

        public bool TrySetVoltage(double Voltage)
        {
            throw new NotImplementedException();
        }

        public override void DetectInstrument()
        {
            throw new NotImplementedException();
        }
    }
}
