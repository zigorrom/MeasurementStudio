using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments
{
    [InstrumentAttribute("KEITHLEY","2602A")]
    public class Keithley2602A:ISourceMeasurementUnit
    {
        public bool TryReadVoltage(out double Voltage)
        {
            throw new NotImplementedException();
        }

        public bool TrySetVoltage(double Voltage)
        {
            throw new NotImplementedException();
        }
    }
}
