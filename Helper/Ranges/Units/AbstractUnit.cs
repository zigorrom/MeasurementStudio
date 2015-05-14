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
        private void Initialize()
        {
            CalculatePrefixVal();
            GenerateUnits();
        }
        private string m_name;
        private double m_prefixVal;
        private void CalculatePrefixVal()
        {
            m_prefixVal = Math.Pow(10, (int)m_prefix);
        }

        private string UnitsFromPrefix(string prefix)
        {
            return prefix + m_name;
        }
        private void GenerateUnits()
        {
            switch (Prefix)
            {
                case UnitPrefixesEnum.YOTTA:
                    Units = UnitsFromPrefix("Y");
                    break;
                case UnitPrefixesEnum.ZETTA:
                    Units = UnitsFromPrefix("Z");
                    break;
                case UnitPrefixesEnum.EXA:
                    Units = UnitsFromPrefix("E");
                    break;
                case UnitPrefixesEnum.PETA:
                    Units = UnitsFromPrefix("P");
                    break;
                case UnitPrefixesEnum.TERA:
                    Units = UnitsFromPrefix("T");
                    break;
                case UnitPrefixesEnum.GIGA:
                    Units = UnitsFromPrefix("G");
                    break;
                case UnitPrefixesEnum.MEGA:
                    Units = UnitsFromPrefix("M");
                    break;
                case UnitPrefixesEnum.KILO:
                    Units = UnitsFromPrefix("k");
                    break;
                case UnitPrefixesEnum.HECTO:
                    Units = UnitsFromPrefix("h");
                    break;
                case UnitPrefixesEnum.DECA:
                    Units = UnitsFromPrefix("da");
                    break;
                case UnitPrefixesEnum.DEFAULT:
                    Units = UnitsFromPrefix("");
                    break;
                case UnitPrefixesEnum.DECI:
                    Units = UnitsFromPrefix("d");
                    break;
                case UnitPrefixesEnum.CENTI:
                    Units = UnitsFromPrefix("c");
                    break;
                case UnitPrefixesEnum.MILLI:
                    Units = UnitsFromPrefix("m");
                    break;
                case UnitPrefixesEnum.MICRO:
                    Units = UnitsFromPrefix("u");
                    break;
                case UnitPrefixesEnum.NANO:
                    Units = UnitsFromPrefix("n");
                    break;
                case UnitPrefixesEnum.PICO:
                    Units = UnitsFromPrefix("p");
                    break;
                case UnitPrefixesEnum.FEMTO:
                    Units = UnitsFromPrefix("f");
                    break;
                case UnitPrefixesEnum.ATTO:
                    Units = UnitsFromPrefix("a");
                    break;
                case UnitPrefixesEnum.ZEPTO:
                    Units = UnitsFromPrefix("z");
                    break;
                case UnitPrefixesEnum.YOCTO:
                    Units = UnitsFromPrefix("y");
                    break;
                default:
                    Units = UnitsFromPrefix("");
                    break;
            }
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
                Initialize();
                OnPropertyChanged("Prefix");
                
            }
        }

        public double GetNumericValue(double magnitude)
        {
            return m_prefixVal * magnitude;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
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
    }
}
