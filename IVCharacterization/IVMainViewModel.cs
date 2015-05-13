using DataVisualization;
using Helper.Ranges;
using Helper.Ranges.RangeHandlers;
using Instruments;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization
{
    public enum IVCharacteristicTypeEnum
    {
        Output,
        Transfer
    }

    public class IVMainViewModel:INotifyPropertyChanged
    {
        private void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }
        private IInstrument m_BackGateSMU;
        private IInstrument m_DrainSourseSMU;
        private IVCharacteristicTypeEnum m_IVCharacteristicType;
        public IVCharacteristicTypeEnum IVCharacteristicType
        {
            get { return m_IVCharacteristicType; }
            set
            {
                if (m_IVCharacteristicType == value)
                    return;
                m_IVCharacteristicType = value;
                OnPropertyChanged("IVCharacteristicType");
            }
        }

        private DoubleRangeBase m_DSVoltageRange;
        public DoubleRangeBase DSVoltageRange
        {
            get { return m_DSVoltageRange; }
            set
            {
                if (m_DSVoltageRange == value) return;
                m_DSVoltageRange = value;
                OnPropertyChanged("DSVoltageRange");
            }
        }

        private DoubleRangeBase m_GSVoltageRange;
        public DoubleRangeBase GSVoltageRange
        {
            get { return m_GSVoltageRange; }
            set
            {
                if (m_GSVoltageRange == value)
                    return;
                m_GSVoltageRange = value;
                OnPropertyChanged("GSVoltageRange");
            }
        }

        private AbstractDoubleRangeHandler m_DSVoltageRangeHandler;
        public AbstractDoubleRangeHandler DSVoltageRangeHandler
        {
            get { return m_DSVoltageRangeHandler; }
            set
            {
                if (m_DSVoltageRangeHandler == value) return;
                m_DSVoltageRangeHandler = value;
                OnPropertyChanged("DSVoltageRangeHandler");
            }
        }
        private AbstractDoubleRangeHandler m_GSVoltageRangeHandler;
        public AbstractDoubleRangeHandler GSVoltageRangeHandler
        {
            get { return m_GSVoltageRangeHandler; }
            set
            {
                if (m_GSVoltageRangeHandler == value) return;
                m_GSVoltageRangeHandler = value;
                OnPropertyChanged("GSVoltageRangeHandler");
            }
        }

        private VisualizationViewModel m_Visualization;
        public VisualizationViewModel Visualization
        {
            get { return m_Visualization; }
            set
            {
                if (m_Visualization == value) return;
                m_Visualization = value;
                OnPropertyChanged("Visualization");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
