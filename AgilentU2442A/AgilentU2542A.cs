using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A
{
    [InstrumentAttribute("Agilent","U2542A")]
    public class AgilentU2542A:AbstractMessageBasedInstrument//,IDAQ
    {
        private AgilentU2542ACommandClass m_commandSet;
        public AgilentU2542ACommandClass CommandSet
        {
            get { return m_commandSet; }
        }

        private Dictionary<ChannelName, AbstractChannel> m_DeviceChannels;

        public AgilentU2542A(string Name,string Alias,string ResourceName):base(Name,Alias,ResourceName)
        {
            Initialize();
        }

        public override void DetectInstrument()
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            if (!IsAlive(true))
                throw new SystemException("Device was not initialized.");
            m_commandSet = new AgilentU2542ACommandClass();
            m_DeviceChannels = new Dictionary<ChannelName,AbstractChannel>();
            m_DeviceChannels.Add(ChannelEnum.AI_CH101, new AnalogInputChannel(ChannelEnum.AI_CH101, this));
            m_DeviceChannels.Add(ChannelEnum.AI_CH102, new AnalogInputChannel(ChannelEnum.AI_CH102, this));
            m_DeviceChannels.Add(ChannelEnum.AI_CH103, new AnalogInputChannel(ChannelEnum.AI_CH103, this));
            m_DeviceChannels.Add(ChannelEnum.AI_CH104, new AnalogInputChannel(ChannelEnum.AI_CH104, this));
        }

        public override void SetBufferSize(int Size)
        {
            base.SetBufferSize(Size);
        }

        public override void Reset()
        {
            SendCommand(CommandSet.CLS());
            SendCommand(CommandSet.RST());
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



        public override void SetTimeout(int p)
        {
            base.SetTimeout(p);
        }
    }
}
