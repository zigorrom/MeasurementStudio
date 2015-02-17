using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentAbstractionModel
{
    public interface IInstrument
    {
        string Name { get; set; }
        string Alias { get; set; }
        string ResourceName { get; set; }
        IInstrumentOwner InstrumentOwner { get; set; }
        InstrumentState State { get; set; }
        void DetectInstrument();
    }
}
