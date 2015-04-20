using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A
{
    public abstract class AbstractChannel
    {
        private string m_NativeChannelName;
        private string m_AliasChannelName;
        private AgilentU2542A m_ParentDevice;
        public AbstractChannel(string NativeChannelName, string AliasChannelName, AgilentU2542A ParentDevice)
        {
            m_NativeChannelName = NativeChannelName;
            m_AliasChannelName = AliasChannelName;
            m_ParentDevice = ParentDevice;
        }

        public string NativeChannelName
        {
            get { return m_NativeChannelName; }
        }

        public string AliasChannelName
        {
            get { return m_AliasChannelName; }
        }
    }
}
