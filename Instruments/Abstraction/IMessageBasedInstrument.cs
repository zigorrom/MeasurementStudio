using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments
{
    interface IMessageBasedInstrument:IDisposable
    {
        bool InitializeDevice();
        bool SendCommand(string Command);
        string GetResponce();
        string Query(string Command);
        bool IsAlive(bool SendIDN);
        //        bool IsAlive { get; }
        bool CheckErrors();
    }
}
