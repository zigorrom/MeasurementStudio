using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments
{
    [InstrumentAttribute("KEITHLEY","24")]//24 - BECAUSE 2400,2430 FITS
    public class Keithley24xx:ISourceMeasurementUnit
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
