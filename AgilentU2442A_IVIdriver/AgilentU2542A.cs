using Agilent.AgilentU254x.Interop;
using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A_IVIdriver
{
    [InstrumentAttribute("Agilent","U2542A")]
    public class AgilentU2542A:IInstrument, IDisposable//AbstractMessageBasedInstrument//,IDAQ
    {
        public AgilentU2542A(string Name, string Alias, string ResourceName)//:base(Name,Alias,ResourceName)
        {
            this.Name = Name;
            this.Alias = Alias;
            this.ResourceName = ResourceName;
            Initialize();

        }

        
        public string Name { get; private set; }
        public string Alias { get; private set; }
        public string ResourceName { get; private set; }

        public AgilentU254x Driver { get; private set; }
       
        private Dictionary<ChannelName, AbstractChannel> m_DeviceChannels;

        private AnalogDataAquisitionController _router;

        public IDataAquisition Aquisition { get { return _router; } }

       
        public void Initialize()
        {
            Driver = new AgilentU254x();
            Driver.Initialize(ResourceName, false, true, "Simulate=false, Cache=false, QueryInstrStatus=true");

            if (!Driver.Initialized)//!IsAlive(true))
                throw new SystemException("Device was not initialized.");
            //m_commandSet = new AgilentU2542ACommandBuilder();
            _router = new AnalogDataAquisitionController(this);
            m_DeviceChannels = new Dictionary<ChannelName,AbstractChannel>();
            
            ///
            ///Analog input channels
            ///
            m_DeviceChannels.Add(ChannelEnum.AI_CH101, new AnalogInputChannel(ChannelEnum.AI_CH101, this));
            m_DeviceChannels.Add(ChannelEnum.AI_CH102, new AnalogInputChannel(ChannelEnum.AI_CH102, this));
            m_DeviceChannels.Add(ChannelEnum.AI_CH103, new AnalogInputChannel(ChannelEnum.AI_CH103, this));
            m_DeviceChannels.Add(ChannelEnum.AI_CH104, new AnalogInputChannel(ChannelEnum.AI_CH104, this));

            ///
            ///Analog output channels
            ///

            //m_DeviceChannels.Add(ChannelEnum.AO_CH201, new AnalogOutputChannel(ChannelEnum.AO_CH201, this));
            //m_DeviceChannels.Add(ChannelEnum.AO_CH202, new AnalogOutputChannel(ChannelEnum.AO_CH202, this));
            
            
            ///
            ///Digital channels 
            ///
            //m_DeviceChannels.Add(ChannelEnum.DIG_CH501, new DigitalChannel(ChannelEnum.DIG_CH501, this));
            //m_DeviceChannels.Add(ChannelEnum.DIG_CH502, new DigitalChannel(ChannelEnum.DIG_CH502, this));
            //m_DeviceChannels.Add(ChannelEnum.DIG_CH503, new DigitalChannel(ChannelEnum.DIG_CH503, this));
            //m_DeviceChannels.Add(ChannelEnum.DIG_CH504, new DigitalChannel(ChannelEnum.DIG_CH504, this));


        }

       
        public AbstractChannel this[ChannelEnum ChannelIdentifier]
        {
            get
            {
                return m_DeviceChannels[ChannelIdentifier];
            }
        }

        public AnalogInputChannel GetAnalogInputChannel(ChannelEnum ChannelIdentifier)
        {
            if (ChannelIdentifier < ChannelEnum.AI_CH101 || ChannelIdentifier > ChannelEnum.AI_CH104)
                throw new ArgumentException("Given channel identifier doesn`t correspond to AnalogIn channel set");
            return m_DeviceChannels[ChannelIdentifier] as AnalogInputChannel;
        }

        public AnalogInputChannel[] GetAnalogInputChannels()
        {
            return m_DeviceChannels
                .Where(x => x.Key.ChannelIdentifier >= ChannelEnum.AI_CH101 && x.Key.ChannelIdentifier <= ChannelEnum.AI_CH104)
                .Select(x => x.Value as AnalogInputChannel)
                .ToArray();

        }

        public AnalogOutputChannel GetAnalogOutputChannel(ChannelEnum ChannelIdentifier)
        {
            if (ChannelIdentifier < ChannelEnum.AO_CH201 || ChannelIdentifier > ChannelEnum.AO_CH202)
                throw new ArgumentException("Given channel identifier doesn`t correspond to AnalogOut channel set");
            return m_DeviceChannels[ChannelIdentifier] as AnalogOutputChannel;
        }

        public DigitalChannel GetDigitalChannel(ChannelEnum ChannelIdentifier)
        {
            if (ChannelIdentifier < ChannelEnum.DIG_CH501 || ChannelIdentifier > ChannelEnum.DIG_CH504)
                throw new ArgumentException("Given channel identifier doesn`t correspond to Digital channel set");
            return m_DeviceChannels[ChannelIdentifier] as DigitalChannel;
        }


        public IInstrumentOwner InstrumentOwner
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public InstrumentState State
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsAlive(bool SendIDN)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void DetectInstrument(object data)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IInstrument other)
        {
            throw new NotImplementedException();
        }


        #region IDisposable Members

        /// <summary>
        /// Internal variable which checks if Dispose has already been called
        /// </summary>
        private Boolean disposed;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(Boolean disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                _router.Dispose();
                Driver.Close();
                //TODO: Managed cleanup code here, while managed refs still valid
            }
            //TODO: Unmanaged cleanup code here

            disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Call the private Dispose(bool) helper and indicate 
            // that we are explicitly disposing
            this.Dispose(true);

            // Tell the garbage collector that the object doesn't require any
            // cleanup when collected since Dispose was called explicitly.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The destructor for the class.
        /// </summary>
        ~AgilentU2542A()
        {
            this.Dispose(false);
        }


        #endregion
        
    }
}
