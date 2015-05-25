using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A
{
    public class ComplexChannelName : IChannelName
    {
        private string m_NativeName;
        public string NativeName
        {
            get
            {
                return m_NativeName;
            }
            private set
            {
                m_NativeName = value;
            }
        }
        private ChannelEnum m_ChannelsIdentifier;
        public ChannelEnum ChannelIdentifier
        {
            get
            {
                return m_ChannelsIdentifier;
            }
            private set
            {
                m_ChannelsIdentifier = value;
            }
        }

        private LinkedList<ChannelName> m_ChannelNameList;

        public ComplexChannelName(ChannelName Ch1, params ChannelName[] Channels)
        {
            Init();
            CreateCommonName(Ch1, Channels);
        }

        private void CreateCommonName(ChannelName Ch1,params ChannelName[] Channels)
        {
            throw new NotImplementedException();
        }

        private void Init()
        {
            m_ChannelNameList = new LinkedList<ChannelName>();
        }

        
    }
}
