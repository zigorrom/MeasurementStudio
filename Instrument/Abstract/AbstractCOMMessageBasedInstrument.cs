using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments
{


    public abstract class AbstractCOMMessageBasedInstrument : IInstrument, IMessageBasedInstrument, IDisposable
    {

        private SerialPort _comPort;
        

        public AbstractCOMMessageBasedInstrument(string Name, string Alias, string COMResource, int BaudRate = 9600)
        {
            InitBase(Name, Alias);
            _comPort = new SerialPort(COMResource, BaudRate);
            //InitializeDevice();
        }

        public AbstractCOMMessageBasedInstrument(string Name, string Alias, string COMResource, int BaudRate, Parity parity)
        {
            InitBase(Name, Alias);
            _comPort = new SerialPort(COMResource, BaudRate, parity);
            //InitializeDevice();
        }

        public AbstractCOMMessageBasedInstrument(string Name, string Alias, string COMResource, int BaudRate, Parity parity, int DataBits)
        {
            InitBase(Name, Alias);
            _comPort = new SerialPort(COMResource, BaudRate, parity, DataBits);
            //InitializeDevice();
        }

        public AbstractCOMMessageBasedInstrument(string Name, string Alias, string COMResource, int BaudRate, Parity parity, int DataBits, StopBits stopBits)
        {
            InitBase(Name, Alias);
            _comPort = new SerialPort(COMResource, BaudRate, parity, DataBits, stopBits);
            //InitializeDevice();
        }

        public AbstractCOMMessageBasedInstrument(string Name, string Alias, string COMResource, int BaudRate, Parity parity, int DataBits, StopBits stopBits, Handshake handshake)
        {
            InitBase(Name, Alias);
            _comPort = new SerialPort(COMResource, BaudRate, parity, DataBits, stopBits) { Handshake = handshake };
            //InitializeDevice();

        }

        public AbstractCOMMessageBasedInstrument(string Name, string Alias, string COMResource, int BaudRate, Parity parity, int DataBits, StopBits stopBits, Handshake handshake, string delimiter)
        {
            InitBase(Name, Alias);
            _comPort = new SerialPort(COMResource, BaudRate, parity, DataBits, stopBits) { Handshake = handshake };
            PortDelimeter = delimiter;
            //InitializeDevice();
        }



        private void InitBase(string Name, string Alias)
        {
            this.Name = Name;
            this.Alias = Alias;
            this.InstrumentOwner = null;
            this.State = InstrumentState.Idle;
            this.IDNqueryString = "*IDN?";
            this.PortDelimeter = "\n\r";
        }


        public string Name
        {
            get;
            private set;
        }

        public string Alias
        {
            get;
            private set;
        }

        public string ResourceName
        {
            get { return _comPort.PortName; }
        }

        protected bool SendIDNrequestOnAliveCheck
        {
            get;
            set;
        }

        protected string IDNqueryString
        {
            get;
            set;
        }

        public IInstrumentOwner InstrumentOwner
        {
            get;
            set;

        }

        public InstrumentState State
        {
            get;
            set;
        }

        public bool IsAlive(bool SendIDN)
        {
            if (_comPort == null)
                return false;
            if (!_comPort.IsOpen)
                return false;
            if (SendIDN)
            {
                if (String.IsNullOrEmpty(Query(IDNqueryString)))
                    return false;
            }
            return true;
        }

        public virtual void Reset()
        {
            throw new NotImplementedException();
        }

        public abstract void DetectInstrument(object data);

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


        public int PortBaudRate
        {
            get { return _comPort.BaudRate; }
            set { _comPort.BaudRate = value; }
        }
        public Parity PortParity
        {
            get { return _comPort.Parity; }
            set { _comPort.Parity = value; }
        }
        public int PortDataBits
        {
            get { return _comPort.DataBits; }
            set { _comPort.DataBits = value; }
        }
        public StopBits PortStopBits
        {
            get { return _comPort.StopBits; }
            set { _comPort.StopBits = value; }
        }
        public Handshake PortHandshake
        {
            get { return _comPort.Handshake; }
            set { _comPort.Handshake = value; }
        }
        public string PortDelimeter { get; set; }

        public bool InitializeDevice()
        {
            try
            {
                //_comPort = new SerialPort(ResourceName);
                _comPort.ReadTimeout = 1000;
                _comPort.WriteTimeout = 1000;
                //_comPort.BaudRate = PortBaudRate;
                //_comPort.Parity = PortParity;
                //_comPort.DataBits = PortDataBits;
                //_comPort.StopBits = PortStopBits;
                //_comPort.Handshake = PortHandshake;
                _comPort.Open();
            }
            catch (Exception e)
            {
                return false;
            }
            if (!IsAlive(SendIDNrequestOnAliveCheck))
                return false;
            return true;
        }

        public bool SendCommand(string Command)
        {
            _comPort.Write(Command);
            return true;
        }

        public string GetResponce()
        {
            return _comPort.ReadTo(PortDelimeter);
        }

        public string Query(string Command)
        {
            if (SendCommand(Command))
                return GetResponce();
            return String.Empty;
        }

        public bool CheckErrors()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
