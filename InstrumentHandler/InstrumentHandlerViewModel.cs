using ExperimentAbstractionModel;
using Helper;
using InstrumentAbstractionModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;


namespace InstrumentHandlerNamespace
{
    
    public sealed partial class InstrumentHandler:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string PropertyName)
        {
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }


        private PermissionTable m_PermissionTable;
        
        
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

        public ICollectionView OwnersCollection
        {
            get
            {
                var s = new CollectionViewSource();
                s.Source = ExperimentsRegistry.Instance.OwnerEnumeration;
                return s.View;
            }
        }

        public ICollectionView InstrumentRuleCollection
        {
            get
            {
                var s = new CollectionViewSource();
                s.Source = m_PermissionTable[CurrentOwner];
                return s.View;
            }
        }




        
    }
    
}
