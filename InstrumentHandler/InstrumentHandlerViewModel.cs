using Helper;
using InstrumentAbstractionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentHandlerNamespace
{
    
    public sealed partial class InstrumentHandler:NotifyPropertyChanged
    {
        private Dictionary<IInstrumentOwner, Dictionary<IInstrument, InstrumentPermission>> m_InstrumentPermissionTable;

        private IInstrumentOwner m_CurrentOwner;
        public IInstrumentOwner CurrentOwner
        {
            get { return m_CurrentOwner; }
            set
            {
                if (m_CurrentOwner == value) return;
                m_CurrentOwner = value;
                OnPropertyChanged("CurrentOwner");
            }
        }



    }
    
}
