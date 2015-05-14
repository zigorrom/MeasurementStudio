using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Helper.Ranges.Units
{
    public abstract class AbstractUnit : IUnits
    {
        private IValueConverter m_converter;
        public AbstractUnit(string UnitName, IValueConverter converter, UnitPrefixesEnum prefix = UnitPrefixesEnum.DEFAULT)
        {
            m_name = UnitName;
            Prefix = prefix;
            m_converter = converter;
            Initialize();

        }

        
        public AbstractUnit(string UnitName, IValueConverter converter)
        {
            m_name = UnitName;
            Prefix = UnitPrefixesEnum.DEFAULT;
            m_converter = converter;
            Initialize();
        }
        private void Initialize()
        {
            m_prefixVal = CalculatePrefixVal((int)Prefix);
            Units = GenerateUnits(Prefix);
        }

            

        private string m_name;
        public string Name { get { return m_name; } }
        private string UnitsFromPrefix(string prefix)
        {
            return prefix + m_name;
        }

        private double m_prefixVal;
        private double CalculatePrefixVal(int PowerOfTen)
        {
            return Math.Pow(10, PowerOfTen);
            //m_prefixVal = Math.Pow(10, PowerOfTen);//(int)m_prefix);
        }

        private string GenerateUnits(UnitPrefixesEnum prefix)
        {
            return (string)m_converter.Convert(prefix, typeof(string), null, CultureInfo.CurrentCulture);
        }

        private UnitPrefixesEnum m_prefix;
        public UnitPrefixesEnum Prefix
        {
            get
            {
                return m_prefix;
            }
            set
            {
                if (m_prefix == value) return;
                m_prefix = value;
                m_prefixVal = CalculatePrefixVal((int)Prefix);
                Units = GenerateUnits(Prefix);
                OnPropertyChanged("Prefix");
                
            }
        }

        public double GetNumericValue(double magnitude)
        {
            return m_prefixVal * magnitude;
        }

        private string m_units;
        public string Units
        {
            get
            {
                return m_units;
            }
            private set
            {
                if (value == m_units) return;
                m_units = value;
                OnPropertyChanged("Units");
            }
        }


        public override string ToString()
        {
            return m_name;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }



        public bool Equals(IUnits other)
        {
            if (Name == other.Name)
                return true;
            return false;
        }


        //public void CastToPrefix(UnitPrefixesEnum prefix)
        //{
        //    if (prefix == Prefix)
        //        return;
        //    var multiplier = CalculatePrefixVal((int)(Prefix - prefix));
            
        //}
    }
}
