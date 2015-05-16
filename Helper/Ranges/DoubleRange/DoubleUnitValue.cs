using Helper.Ranges.Units;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.DoubleRange
{
    public class DoubleUnitValue:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) 
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }


        private string m_UnitName;
        private double m_Magnitude;
        private UnitPrefixesEnum m_UnitPrefix;
        private double m_PrefixValue;
        private double m_NumericValue;

        public DoubleUnitValue(string UnitName, double Magnitude, UnitPrefixesEnum Prefix)
        {
            Initialize(UnitName, Magnitude, Prefix);
        }

        public DoubleUnitValue(string UnitName, double Magnitude)
        {
            Initialize(UnitName, Magnitude, UnitPrefixesEnum.DEFAULT);
        }

        public DoubleUnitValue(string UnitName)
        {
            Initialize(UnitName, 0, UnitPrefixesEnum.DEFAULT);
        }
        private void Initialize(string UnitName, double Magnitude, UnitPrefixesEnum Prefix)
        {
            this.m_UnitName = UnitName;
            this.Prefix = Prefix;
            this.Magnitude = Magnitude;
        }

        public string UnitName
        {
            get { return m_UnitName; }
        }

        public double NumericValue
        {
            get { return m_NumericValue; }
            private set
            {
                if (m_NumericValue == value)
                    return;
                m_NumericValue = value;
                OnPropertyChanged("NumericValue");
            }
        }


        public double Magnitude
        {
            get { return m_Magnitude; }
            set
            {
                if(m_Magnitude == value)
                    return;
                m_Magnitude = value;
                NumericValue = Magnitude * m_PrefixValue;
                OnPropertyChanged("Magnitude");
            }
        }

        public UnitPrefixesEnum Prefix
        {
            get { return m_UnitPrefix; }
            set
            {
                if (m_UnitPrefix == value) return;
                m_UnitPrefix = value;
                m_PrefixValue = ConvertPrefixToDouble(m_UnitPrefix);
                OnPropertyChanged("Prefix");
            }
        }

        public void CastToPrefix(UnitPrefixesEnum prefix)
        {
            //var oldPref = m_PrefixValue;
            //var oldNumVal = NumericValue;
            Prefix = prefix;
            Magnitude = NumericValue / m_PrefixValue; //oldNumVal / m_PrefixValue;
        }

        private double ConvertPrefixToDouble(UnitPrefixesEnum unitPrefix)
        {
            switch (unitPrefix)
            {
                case UnitPrefixesEnum.YOTTA: return UnitPrefixesValues.YOTTA;
                case UnitPrefixesEnum.ZETTA: return UnitPrefixesValues.ZETTA;
                case UnitPrefixesEnum.EXA: return UnitPrefixesValues.EXA;
                case UnitPrefixesEnum.PETA: return UnitPrefixesValues.PETA;
                case UnitPrefixesEnum.TERA: return UnitPrefixesValues.TERA;
                case UnitPrefixesEnum.GIGA: return UnitPrefixesValues.GIGA;
                case UnitPrefixesEnum.MEGA: return UnitPrefixesValues.MEGA;
                case UnitPrefixesEnum.KILO: return UnitPrefixesValues.KILO;
                case UnitPrefixesEnum.HECTO: return UnitPrefixesValues.HECTO;
                case UnitPrefixesEnum.DECA: return UnitPrefixesValues.DECA;
                case UnitPrefixesEnum.DEFAULT: return UnitPrefixesValues.DEFAULT;
                case UnitPrefixesEnum.DECI: return UnitPrefixesValues.DECI;
                case UnitPrefixesEnum.CENTI: return UnitPrefixesValues.CENTI;
                case UnitPrefixesEnum.MILLI: return UnitPrefixesValues.MILLI;
                case UnitPrefixesEnum.MICRO: return UnitPrefixesValues.MICRO;
                case UnitPrefixesEnum.NANO: return UnitPrefixesValues.NANO;
                case UnitPrefixesEnum.PICO: return UnitPrefixesValues.PICO;
                case UnitPrefixesEnum.FEMTO: return UnitPrefixesValues.FEMTO;
                case UnitPrefixesEnum.ATTO: return UnitPrefixesValues.ATTO;
                case UnitPrefixesEnum.ZEPTO: return UnitPrefixesValues.ZEPTO;
                case UnitPrefixesEnum.YOCTO: return UnitPrefixesValues.YOCTO;
                default: return UnitPrefixesValues.DEFAULT;
            }
        }
    }
}
