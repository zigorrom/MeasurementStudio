using NationalInstruments.VisaNS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments
{
    public abstract class AbstractMessageBasedInstrument:IInstrument,IMessageBasedInstrument,IDisposable//,IOwnedInstrument
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
        ~AbstractMessageBasedInstrument()
        {
            this.Dispose();
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
            catch (VisaException e)
            {
                OnVisaException(e);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public virtual void AssertSession()
        {
            if(!IsAlive(false))
                throw new ArgumentNullException("Message session is not initialized!");
        }

        public bool CheckErrors()
        {
            throw new NotImplementedException();
        }

        public virtual bool SendCommand(string Command)
        {
            try
            {
               // AssertSession();
                m_session.Write(Command);
            }
            catch (VisaException e)
            {
                OnVisaException(e);
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

        public virtual string GetResponce()
        {
            var responce = "";
            try
            {
                //AssertSession();
                responce = m_session.ReadString();
            }
            catch (VisaException e)
            {
                OnVisaException(e);
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

        public virtual string Query(string Command)
        {
            //AssertSession();
            var resp = "";
            try
            {
                resp = m_session.Query(Command);
            }
            //catch (ArgumentNullException e)
            //{
            //    return string.Empty;
            //}
            catch (VisaException e)
            {
                OnVisaException(e);
            }
            catch (Exception e)
            {
                throw;
                //return string.Empty;
            }
            return resp;
        }

        private void OnVisaException(VisaException e)
        {
            //this.Dispose();
            throw new Exception("Device doesn`t respond");
            //InitializeDevice();
            //Reset();
            //if (!IsAlive(true))
            //    throw new Exception("Device doesn`t respond after reset");
            //throw new Exception("System reset happened");
        }
        
        protected bool TryConvert(string s, out double Value)
        {
            Value = 0;
            var numForm = new NumberFormatInfo() { NumberDecimalSeparator = ".", NumberGroupSeparator = "" };
            if (double.TryParse(s, NumberStyles.Float, numForm, out Value))
                return true;
            return false;
        }

        public virtual void Dispose()
        {
            m_session.Dispose();
            m_session = null;
        }


        public virtual bool IsAlive(bool SendIDN)
        {
            if (m_session == null)
                return false;
            if (SendIDN)
                if (String.IsNullOrEmpty(Query("*IDN?")))
                    return false;
            return true;
        }


        public abstract void Reset();
        
    }
}
