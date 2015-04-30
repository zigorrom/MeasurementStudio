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

        private Dictionary<ChannelName, AnalogInputChannel> m_AnalogInputChannels;

        public AgilentU2542A(string Name,string Alias,string ResourceName):base(Name,Alias,ResourceName)
        {
            m_commandSet = new AgilentU2542ACommandClass();
            m_AnalogInputChannels = new Dictionary<ChannelName, AnalogInputChannel>();
            m_AnalogInputChannels.Add(ChannelEnum.AI_CH101, new AnalogInputChannel(ChannelEnum.AI_CH101, this));
            m_AnalogInputChannels.Add(ChannelEnum.AI_CH102, new AnalogInputChannel(ChannelEnum.AI_CH102, this));
            m_AnalogInputChannels.Add(ChannelEnum.AI_CH103, new AnalogInputChannel(ChannelEnum.AI_CH103, this));
            m_AnalogInputChannels.Add(ChannelEnum.AI_CH104, new AnalogInputChannel(ChannelEnum.AI_CH104, this));

        }



        public override void DetectInstrument()
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
