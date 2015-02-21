using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentHandlerNamespace
{
    public sealed class InstrumentPermission
    {
        public InstrumentPermission()
        { CanUse = false; }
        //private bool m_CanUse;
        public bool CanUse { get; set; }
    }
}
