using InstrumentAbstractionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentHandlerNamespace
{
    public sealed class PermissionTable
    {
        public PermissionTable()
        {
            m_InstrumentPermissionTable = new Dictionary<IInstrumentOwner, Dictionary<IInstrument, InstrumentPermission>>();
        }

        private Dictionary<IInstrumentOwner, Dictionary<IInstrument, InstrumentPermission>> m_InstrumentPermissionTable;

        public Dictionary<IInstrument,InstrumentPermission> this[IInstrumentOwner owner]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public InstrumentPermission this[IInstrumentOwner owner,IInstrument instr]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }



    }
}
