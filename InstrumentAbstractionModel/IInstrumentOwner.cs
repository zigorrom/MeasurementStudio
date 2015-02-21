using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace InstrumentAbstractionModel
{
    
    public interface IInstrumentOwner:IEquatable<IInstrumentOwner>
    {
        string Name { get; }
    }
}
