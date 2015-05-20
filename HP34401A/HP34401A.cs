using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP34401A
{
    [InstrumentAttribute("HEWLETT-PACKARD", "34401A")]
    public class HP34401A : AbstractMessageBasedInstrument, IMultimeter
    {
        public HP34401A(string Name, string Alias, string ResourceName)
            : base(Name, Alias, ResourceName)
        {

        }

        public override bool InitializeDevice()
        {
            if (base.InitializeDevice())
            {
                SendCommand("*CLS");
                SendCommand("*RST");
                //Set 1 measurement for read
                SendCommand("*ESE 1");
                //enable auto-output off
                SendCommand("*SRE 32");
                //enable source as Voltage
                SendCommand("*OPC");
                //enable sensing as Voltage
                return true;
            }
            return false;
        }

        public bool TryReadVoltage(out double Voltage)
        {
            Voltage = 0;
            SendCommand("*SRE 32");
            var result = Query("MEAS:VOLT:DC?");
            if (String.IsNullOrEmpty(result))
                return false;
            if (TryConvert(result, out Voltage))
                return true;


            return false;
        }

        public override void DetectInstrument()
        {
            throw new NotImplementedException();
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }

        //public override AbstractCommandBuilder CommandSet
        //{
        //    get { throw new NotImplementedException(); }
        //}
    }
}
