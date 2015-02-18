using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentAbstractionModel
{
    public interface IMultimeter
    {
        void InitDevice();
        bool TryReadVoltage(out double Voltage);
    }
}
