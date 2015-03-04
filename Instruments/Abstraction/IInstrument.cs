using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments
{
    
    public interface IInstrument:IEquatable<IInstrument>
    {
        string Name { get;  }
        string Alias { get;  }
        string ResourceName { get;  }
        IInstrumentOwner InstrumentOwner { get; set; }
        InstrumentState State { get; set; }
        void DetectInstrument();
    }
}
