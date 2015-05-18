using DataVisualization;
using Helper.Ranges;
using Helper.Ranges.DoubleRange;
using Helper.Ranges.RangeHandlers;
using Helper.Ranges.SimpleRangeControl;
using Instruments;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization
{
    public struct IVCharacteristicNames
    {
        public const string OutputCharacteristic = "Output";
        public const string TransferCharacteristic = "Transfer";
    }

    public enum IVCharacteristicTypeEnum
    {
        Output,
        Transfer
    }

    public class IVMainViewModel : INotifyPropertyChanged
    {
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
               // OnChangeIVCharacterizationViewModel(value);
                //OnPropertyChanged("IVCharacteristicType");
            }
        }

        private RangeHandlerViewModel m_DSRangeHandlerViewModel;
        public RangeHandlerViewModel DSRangeHandlerViewModel
        {
            get { return m_DSRangeHandlerViewModel; }
            private set
            {
                SetField(ref m_DSRangeHandlerViewModel, value, "DSRangeHandlerViewModel");
            }
        }

        private RangeHandlerViewModel m_GSRangeHandlerViewModel;
        public RangeHandlerViewModel GSRangeHandlerViewModel
        {
            get { return m_GSRangeHandlerViewModel; }
            private set
            {
                SetField(ref m_GSRangeHandlerViewModel, value, "GSRangeHandlerViewModel");
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

        public RangeViewModel DSRangeViewModel { get; set; }
        public RangeViewModel GSRangeViewModel { get; set; }

        public IVMainViewModel(IVCharacteristicTypeEnum characteristicType)
        {
            m_IVCharacteristicType = characteristicType;
            DSRangeViewModel = new RangeViewModel(new Voltage(), new Voltage(), new Voltage());
            GSRangeViewModel = new RangeViewModel(new Voltage(), new Voltage(), new Voltage());

            DSRangeHandlerViewModel = new RangeHandlerViewModel();
            GSRangeHandlerViewModel = new RangeHandlerViewModel();
            Visualization = new VisualizationViewModel();
            Visualization.HorizontalAxisLabel = "Gate Voltage, Vg(V)";
            Visualization.VertivalAxisLabel = "Drain Current, Id(A)";
            Visualization.HeaderLabel = "IV Characterization";
            
        }



        public event EventHandler<IVCharacteristicTypeEnum> ChangeIVCharacterizationViewModel;
        private void OnChangeIVCharacterizationViewModel(IVCharacteristicTypeEnum CharacteristicType)
        {
            var handler = ChangeIVCharacterizationViewModel;
            if (handler != null)
                handler(this, CharacteristicType);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetField<ST>(ref ST field, ST value, string propertyName)
        {
            if (EqualityComparer<ST>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        private void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
