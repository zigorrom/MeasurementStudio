using Agilent.AgilentU254x.Interop;
using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2542ANamespace
{
    public class AgilentU2542A:AgilentU254xClass, IInstrument
    {
        public AgilentU2542A(string Name, string Alias, string ResourceName):base()
        {
            Initialize(ResourceName, true, true);
        }




        private string m_name;
        private string m_alias;
        private string m_resourceName;
        private IInstrumentOwner m_owner;
        private InstrumentState m_state;

        public string Name
        {
            get { return m_name; }
        }

        public string Alias
        {
            get { return m_alias; }
        }

        public string ResourceName
        {
            get { return m_resourceName; }
        }

        public IInstrumentOwner InstrumentOwner
        {
            get
            {
                return m_owner;
            }
            set
            {
                m_owner = value;
            }
        }

        public InstrumentState State
        {
            get
            {
                return m_state;
            }
            set
            {
                m_state = value;
            }
        }

        public bool IsAlive(bool SendIDN)
        {
            return Initialized;
        }

        public void DetectInstrument(object data)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IInstrument other)
        {
            if (other.Alias == Alias)
                if (other.Name == Name)
                    if (other.ResourceName == ResourceName)
                        if (other.State == State)
                            if (Object.ReferenceEquals(this, other))
                                //if (other.InstrumentOwner.Name == InstrumentOwner.Name)
                                return true;
            return false;
        }
    }
}
