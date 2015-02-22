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

        public void AddPermission(IInstrumentOwner owner,IInstrument instr, InstrumentPermission permission)
        {
             Dictionary<IInstrument,InstrumentPermission> PermList;
             if(!m_InstrumentPermissionTable.ContainsKey(owner))
             {
                 PermList = new Dictionary<IInstrument,InstrumentPermission>();
                 m_InstrumentPermissionTable.Add(owner,PermList);
             }
             else
             {
                 PermList = m_InstrumentPermissionTable[owner];
             }
                 
             if (PermList.ContainsKey(instr))
                     throw new InvalidOperationException("Permission for this owner and instrument are already exists");
             else
                 PermList.Add(instr,permission);

             
        }
        public void AddPermission(IInstrumentOwner owner,IInstrument instr)
        {
            AddPermission(owner, instr, new InstrumentPermission(false));
        }

        public Dictionary<IInstrument,InstrumentPermission> this[IInstrumentOwner owner]
        {
            get { return m_InstrumentPermissionTable[owner]; }
            set { throw new NotImplementedException(); }
        }

        public InstrumentPermission this[IInstrumentOwner owner,IInstrument instr]
        {
            get
            {
                if (!m_InstrumentPermissionTable.ContainsKey(owner))
                    return null;
                var PermList = m_InstrumentPermissionTable[owner];
                if (PermList.ContainsKey(instr))
                    return PermList[instr];
                return null;
            }
            set { throw new NotImplementedException(); }
        }



    }
}
