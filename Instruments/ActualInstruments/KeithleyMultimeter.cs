using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments
{
    [InstrumentAttribute("NDCV","")]
    public class KeithleyMultimeter:AbstractMessageBasedInstrument, IMultimeter
    {
        public KeithleyMultimeter(string Name,string Alias, string ResourceName):base(Name,Alias,ResourceName)
        {

        }
        public bool TryReadVoltage(out double Voltage)
        {
            Voltage = 0;
            var result = Query("OUTR?1");
            if (String.IsNullOrEmpty(result))
                return false;
            if (TryConvert(result.Substring(4), out Voltage))
                return true;
            return false;
            
        }

        public override void DetectInstrument()
        {
            throw new NotImplementedException();
        }
    }
}
