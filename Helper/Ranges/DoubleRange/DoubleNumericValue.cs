using Helper.Ranges.Units;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.DoubleRange
{
    public class DoubleNumericValue:INotifyPropertyChanged
    {
        public DoubleNumericValue(IUnits units)
        {
            m_NumericValue = 0;
            m_Magnitude = 0;
            m_Units = units;
        }

        private double m_NumericValue;
        private double m_Magnitude;
        private IUnits m_Units;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
