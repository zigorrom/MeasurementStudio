using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments
{
    public interface ILockInAmplifier
    {
        void InitDevice();
        bool ReadSignal(out double Signal);
    }
}
