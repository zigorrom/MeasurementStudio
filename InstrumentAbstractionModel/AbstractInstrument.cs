using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentAbstractionModel
{
    public abstract class AbstractInstrument:IInstrument
    {
        public AbstractInstrument(string Name, string Alias, string ResourceName)
        {
            m_name = Name;
            m_alias = Alias;
            m_resourceName = ResourceName;
        }

        private string m_name;
        public string Name
        {
            get { return m_name; }
            protected set { m_name = value; }
            
        }

        private string m_alias;
        public string Alias
        {
            get { return m_alias; }
            protected set { m_alias = value; }
            
        }

        private string m_resourceName;
        public string ResourceName //{ get; }
        {
            get { return m_resourceName; }
            protected set { m_resourceName = value; }
        }

        private IInstrumentOwner m_instrumentOwner;
        public IInstrumentOwner InstrumentOwner
        {
            get { return m_instrumentOwner; }
            set { m_instrumentOwner = value; }
            
        }

        private InstrumentState m_instrumentState;
        public InstrumentState State
        {
            get { return m_instrumentState; }
            set { m_instrumentState = value; }
        }

        public abstract void DetectInstrument();


        public bool Equals(IInstrument other)
        {
            if (other.Alias == Alias)
                if (other.Name == Name)
                    if (other.ResourceName == ResourceName)
                        if (other.State == State)
                            if (other.InstrumentOwner.Name == InstrumentOwner.Name)
                                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return String.Format("{0},{1},{2},{3},{4}", Alias, Name, ResourceName, State, InstrumentOwner.Name).GetHashCode();
        }
    }
}
