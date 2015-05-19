using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class InstrumentAttribute:Attribute
    {
        private string m_Manufacturer;
        public string Manufacturer
        {
            get { return m_Manufacturer; }
            private set { m_Manufacturer = value; }
        }

        private string m_Model;
        public string Model
        {
            get { return m_Model; }
            private set { m_Model = value; }
        }

        public InstrumentAttribute(string Manufacturer, string Version)
        {
            m_Manufacturer = Manufacturer;
            m_Model = Version;
        }
        public bool FitsToIDN(string IDNstring)
        {
            var IDNfields = IDNstring.Split(new char[1] { ',' });
            if (IDNfields.Length < 2)
            {
                if (IDNstring.Contains(Manufacturer))
                    return true;
                return false;
            }
            if (IDNfields[0].Contains(Manufacturer))
                if (IDNfields[1].Contains(Model))
                    return true;
            return false;
        }
        
    }
}
