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
            m_Units.PropertyChanged += m_Units_PropertyChanged;
        }

        void m_Units_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Units")
                m_NumericValue = m_Units.GetNumericValue(m_Magnitude);
        }

        private double m_NumericValue;
        private double m_Magnitude;
        private IUnits m_Units;

        public double Magnitude
        {
            get { return m_Magnitude; }
            set
            {
                if (m_Magnitude == value) return;
                      m_Magnitude = value;
                      m_NumericValue = m_Units.GetNumericValue(m_Magnitude);
                OnPropertyChanged("Magnitude");
            }
        }

        public double NumericValue
        {
            get { return m_NumericValue; }
        }

        public IUnits Units
        {
            get { return m_Units; }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }

        public static implicit operator double(DoubleNumericValue d)
        {
            return d.NumericValue;
        }

    }
}
