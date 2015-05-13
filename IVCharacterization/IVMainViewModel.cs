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
        private DoubleRangeBase m_DSVolrageRange;
        private DoubleRangeBase m_GsVoltageRange;
        private AbstractDoubleRangeHandler m_DSVoltageRangeHandler;
        private AbstractDoubleRangeHandler m_GSVoltageRangeHandler;
        private VisualizationViewModel m_Visualization;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
