using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments
{
    public interface IInstrumentFactory
    {
        IInstrument CreateInstrument(string Name, string Alias, string ResourceName);
        bool CreateInstrument<T>(out T instrument, string Name, string Alias, string ResourceName) where T : IInstrument;
        bool FitsIDN(string IDN);

    }
}
