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

        private bool m_HasChannels;

        public bool HasChannels
        {
            get { return m_HasChannels; }
            set { m_HasChannels = value; }
        }

        private Type[] m_ChannelTypes;

        public Type[] ChannelTypes
        {
            get { return m_ChannelTypes; }
            set { m_ChannelTypes = value; }
        }


        public InstrumentAttribute(string Manufacturer, string Version)
        {
            m_Manufacturer = Manufacturer;
            m_Model = Version;
            m_HasChannels = false;
            m_ChannelTypes = null;
        }

        public InstrumentAttribute(string Manufacturer, string Version, params Type[] ChannelTypes)
        {
            m_Manufacturer = Manufacturer;
            m_Model = Version;
            m_HasChannels = false;
            m_ChannelTypes = null;
            if (ChannelTypes.Length > 0)
            {
                m_HasChannels = true;
                m_ChannelTypes = ChannelTypes;
            }
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
