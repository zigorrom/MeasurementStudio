using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.Units
{
    public abstract class AbstractUnit : IUnits
    { 
      
        public AbstractUnit(string UnitName, UnitPrefixesEnum prefix = UnitPrefixesEnum.DEFAULT)
        {
            m_name = UnitName;
            Prefix = prefix;
            Initialize();
        }

        
        public AbstractUnit(string UnitName)
        {
            m_name = UnitName;
            Prefix = UnitPrefixesEnum.DEFAULT;
            Initialize();
        }
        private void Initialize(UnitPrefixesEnum MinPrefixParam =  UnitPrefixesEnum.YOCTO, UnitPrefixesEnum MaxPrefixParam = UnitPrefixesEnum.YOTTA)
        {
            CalculatePrefixVal();
            Units = GenerateUnits(Prefix);
            MinPrefix = MinPrefixParam;
            MaxPrefix = MaxPrefixParam;
           // InitializeUnitSource();
            //unitSource = new string[]
            //m_unitSource = new Dictionary<string, UnitPrefixesEnum>();
            //GenerateUnitSource();
        }

            

        private string m_name;
        private string UnitsFromPrefix(string prefix)
        {
            return prefix + m_name;
        }

        private double m_prefixVal;
        private void CalculatePrefixVal()
        {
            m_prefixVal = Math.Pow(10, (int)m_prefix);
        }

        private string GenerateUnits(UnitPrefixesEnum prefix)
        {
            switch (prefix)
            {
                case UnitPrefixesEnum.YOTTA:
                    return UnitsFromPrefix("Y");
                case UnitPrefixesEnum.ZETTA:
                    return UnitsFromPrefix("Z");
                case UnitPrefixesEnum.EXA:
                    return UnitsFromPrefix("E");
                case UnitPrefixesEnum.PETA:
                    return UnitsFromPrefix("P");
                case UnitPrefixesEnum.TERA:
                    return UnitsFromPrefix("T");
                case UnitPrefixesEnum.GIGA:
                    return UnitsFromPrefix("G");
                case UnitPrefixesEnum.MEGA:
                    return UnitsFromPrefix("M");
                case UnitPrefixesEnum.KILO:
                    return UnitsFromPrefix("k");
                case UnitPrefixesEnum.HECTO:
                    return UnitsFromPrefix("h");
                case UnitPrefixesEnum.DECA:
                    return UnitsFromPrefix("da");
                case UnitPrefixesEnum.DEFAULT:
                    return UnitsFromPrefix("");
                case UnitPrefixesEnum.DECI:
                    return UnitsFromPrefix("d");
                case UnitPrefixesEnum.CENTI:
                    return UnitsFromPrefix("c");
                case UnitPrefixesEnum.MILLI:
                    return UnitsFromPrefix("m");
                case UnitPrefixesEnum.MICRO:
                    return UnitsFromPrefix("u");
                case UnitPrefixesEnum.NANO:
                    return UnitsFromPrefix("n");
                case UnitPrefixesEnum.PICO:
                    return UnitsFromPrefix("p");
                case UnitPrefixesEnum.FEMTO:
                    return UnitsFromPrefix("f");
                case UnitPrefixesEnum.ATTO:
                    return UnitsFromPrefix("a");
                case UnitPrefixesEnum.ZEPTO:
                    return UnitsFromPrefix("z");
                case UnitPrefixesEnum.YOCTO:
                    return UnitsFromPrefix("y");
                default:
                    return UnitsFromPrefix("");
            }
        }

        private UnitPrefixesEnum m_MinPrefix;
        public UnitPrefixesEnum MinPrefix
        {
            get { return m_MinPrefix; }
            set { m_MinPrefix = value; }
        }

        private UnitPrefixesEnum m_MaxPrefix;
        public UnitPrefixesEnum MaxPrefix
        {
            get { return m_MaxPrefix; }
            set { m_MaxPrefix = value; }
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
                CalculatePrefixVal();
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



        
    }
}
