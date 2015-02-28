using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments
{
    public interface ISourceMeasurementUnit
    {
        bool TryReadVoltage(out double Voltage);
        bool TrySetVoltage(double Voltage);
    }
}
