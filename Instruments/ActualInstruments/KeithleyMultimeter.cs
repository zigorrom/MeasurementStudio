using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments
{
    [InstrumentAttribute("NDCV","")]
    public class KeithleyMultimeter:IMultimeter
    {
        public bool TryReadVoltage(out double Voltage)
        {
            throw new NotImplementedException();
        }
    }
}
