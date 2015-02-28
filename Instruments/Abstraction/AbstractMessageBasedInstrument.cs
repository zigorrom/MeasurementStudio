using NationalInstruments.VisaNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments
{
    public abstract class AbstractMessageBasedInstrument:IInstrument,IMessageBasedInstrument//,IOwnedInstrument
    {
        public AbstractMessageBasedInstrument(string Name, string Alias, string ResourceName)
        {
            m_name = Name;
            m_alias = Alias;
            m_resourceName = ResourceName;
            m_instrumentState = InstrumentState.Idle;
            m_instrumentOwner = null;
            InitializeDevice();
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
                            if (Object.ReferenceEquals(this, other))
                                //if (other.InstrumentOwner.Name == InstrumentOwner.Name)
                                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return String.Format("{0},{1},{2},{3}", Alias, Name, ResourceName, State/*, InstrumentOwner.Name*/).GetHashCode();
        }

        private MessageBasedSession m_session;
        public virtual bool InitializeDevice()
        {
            try
            {
                m_session = new MessageBasedSession(m_resourceName, AccessModes.ExclusiveLock, 10000);
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }

        private void AssertSession()
        {
            if (m_session == null)
                throw new ArgumentNullException("Message session is not initialized!");
        }

        public bool SendCommand(string Command)
        {
            try
            {
                AssertSession();
                m_session.Write(Command);
            }
            catch (ArgumentNullException e)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public string GetResponce()
        {
            var responce = "";
            try
            {
                AssertSession();
                responce = m_session.ReadString();
            }
            catch (ArgumentNullException e)
            {
                return String.Empty;
            }
            catch(Exception e)
            {
                return String.Empty;
            }
            return responce;
        }

        public string Query(string Command)
        {
            AssertSession();
            var resp = "";
            try
            {
                resp = m_session.Query(Command);
            }
            catch (ArgumentNullException e)
            {
                return string.Empty;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
            return resp;
        }

        public bool IsAlive
        {
            get
            {
                var idn = Query("*IDN?");
                if (String.IsNullOrEmpty(idn))
                    return false;
                return true;
            }
        }
    }
}
