using Helper.Ranges.Units;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Helper.Ranges.DoubleRange
{
    public abstract class DoubleUnitValue:DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) 
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected bool SetField<ST>(ref ST field, ST value, string propertyName)
        {
            if (EqualityComparer<ST>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }


        private string m_UnitName;
        private const string UnitNamePropertyName = "UnitName";
        private double m_Magnitude;
        private const string MagnitudePropertyName = "Magnitude";
        private UnitPrefixesEnum m_UnitPrefix;
        private const string PrefixPropertyName = "Prefix";
        private double m_PrefixValue;
       
        private double m_NumericValue;
        private const string NumericValuePropertyName = "NumericValue";
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
            this.m_PrefixValue = 1;
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
            set
            {
                if(SetField(ref m_NumericValue, value,NumericValuePropertyName))
                {
                    var magnitude = NumericValue / m_PrefixValue;
                    SetField(ref m_Magnitude, magnitude, MagnitudePropertyName);
                }
            }

            
        }


        public double Magnitude
        {
            get { return m_Magnitude; }
            set
            {
                if(SetField(ref m_Magnitude, value, MagnitudePropertyName))
                {
                    var numVal = Magnitude * m_PrefixValue;
                    SetField(ref m_NumericValue, numVal, NumericValuePropertyName);
                }

            }
        }

        public UnitPrefixesEnum Prefix
        {
            get { return m_UnitPrefix; }
            set
            {
                if (m_UnitPrefix == value) 
                    return;
                m_UnitPrefix = value;
                m_PrefixValue = UnitPrefixesValues.ConvertFromPrefixToDouble(m_UnitPrefix);// ConvertPrefixToDouble(m_UnitPrefix);
                OnPropertyChanged(PrefixPropertyName);
            }
        }

        public void CastToPrefix(UnitPrefixesEnum prefix)
        {
            //var oldPref = m_PrefixValue;
            //var oldNumVal = NumericValue;
            Prefix = prefix;
            Magnitude = NumericValue / m_PrefixValue; //oldNumVal / m_PrefixValue;
        }


        
    }
}
