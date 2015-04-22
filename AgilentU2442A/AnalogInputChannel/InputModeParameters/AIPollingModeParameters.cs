using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A
{
    public class AIPollingModeParameters:AbstractNotifyPropertyChanged
    {
        private VoltageRangeEnum m_VoltageRange;

        public VoltageRangeEnum VoltageRange
        {
            get { return m_VoltageRange; }
            set
            {
                if (m_VoltageRange == value)
                    return;
                m_VoltageRange = value;
                OnPropertyChanged("VoltageRange");
            }
        }

        private PolarityEnum m_VoltagePolarity;

        public PolarityEnum VoltagePolarity
        {
            get { return m_VoltagePolarity; }
            set
            {
                if (m_VoltagePolarity == value)
                    return;
                m_VoltagePolarity = value;
                OnPropertyChanged("VoltagePolarity");
            }
        }

        private int m_AveragingNumber;

        public int AveragingNumber
        {
            get { return m_AveragingNumber; }
            set
            {
                if (m_AveragingNumber == value)
                    return;
                m_AveragingNumber = value;
                OnPropertyChanged("AveragingNumber");
            }

        }

    }
}
