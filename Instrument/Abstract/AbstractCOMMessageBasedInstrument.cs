using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments
{
    
    public enum Delimiter
    {
        CR_LF,
        CR
    }

    public abstract class AbstractCOMMessageBasedInstrument:IInstrument,IMessageBasedInstrument,IDisposable
    {

        private SerialPort _comPort;
        private string _delimiter;

        public AbstractCOMMessageBasedInstrument(string Name, string Alias, string COMResource, int BaudRate, Parity parity, int DataBits, StopBits stopBits, Handshake handshake, Delimiter delimiter)
            
        {
            this.Name = Name;
            this.Alias = Alias;
            this.ResourceName = COMResource;
            this.InstrumentOwner = null;
            this.State = InstrumentState.Idle;


            PortDelimeter = delimiter;
            switch (PortDelimeter)
            {
                case Delimiter.CR:
                    _delimiter = "\r";
                    break;
                case Delimiter.CR_LF:
                default:
                    _delimiter = "\r\n";
                    break;
            }

            PortBaudRate = BaudRate;
            PortParity = parity;
            PortDataBits = DataBits;
            PortStopBits = stopBits;
            PortHandshake = handshake;

            InitializeDevice();
        }

        //public AbstractCOMMessageBasedInstrument(string Name, string Alias, string COMResource)
        //{
        //    this.Name = Name;
        //    this.Alias = Alias;
        //    this.ResourceName = COMResource;
        //    this.InstrumentOwner = null;
        //    this.State = InstrumentState.Idle;
        //    InitializeDevice();
        //    //_comPort.BaudRate
        //    //_comPort.Handshake = Handshake.
        //    //_comPort.Parity
        //    //_comPort.PortName
        //    //_comPort.StopBits
            
        //}

       
        //private string _name;
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
            get;
            private set;
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
            if(SendIDN)
            {
                if (String.IsNullOrEmpty(Query("*IDN?")))
                    return false;
            }
            return true;
        }

        public void Reset()
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


        public int PortBaudRate { get; set; }
        public Parity PortParity { get; set; }
        public int PortDataBits { get; set; }
        public StopBits PortStopBits { get; set; }
        public Handshake PortHandshake { get; set; }
        public Delimiter PortDelimeter { get; set; }

        public bool InitializeDevice()
        {
            try
            {
                _comPort = new SerialPort(ResourceName);
                _comPort.ReadTimeout = 1000;
                _comPort.WriteTimeout = 1000;
                _comPort.BaudRate = PortBaudRate;
                _comPort.Parity = PortParity;
                _comPort.DataBits = PortDataBits;
                _comPort.StopBits = PortStopBits;
                _comPort.Handshake = PortHandshake;
                _comPort.Open();
            }
            catch (Exception e)
            {
                return false;
            }
            if (!IsAlive(true))
                return false;
            return true;
        }

        public bool SendCommand(string Command)
        {
            try
            {
                _comPort.Write(Command);
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }

        public string GetResponce()
        {
            try
            {
                return _comPort.ReadTo(_delimiter);
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
            //throw new NotImplementedException();
            //_comPort.rea
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
