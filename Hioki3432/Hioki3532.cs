using Instruments;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hioki3432
{
    public class Hioki3532 : AbstractCOMMessageBasedInstrument
    {
        public Hioki3532(string Name, string Alias, string ComResource, int BaudRate, Parity parity, int DataBits, StopBits stopBits, Handshake handshake, Delimiter delimiter)
            : base(Name, Alias, ComResource, BaudRate, parity, DataBits, stopBits, handshake, delimiter)
        {

        }

        public override void DetectInstrument(object data)
        {
            throw new NotImplementedException();
        }
    }
}
