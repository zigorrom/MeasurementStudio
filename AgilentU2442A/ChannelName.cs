using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilentU2442A
{
    public class ChannelName
    {
        private string m_NativeName;

        public string NativeName
        {
            get { return m_NativeName; }
            set { m_NativeName = value; }
        }

    }
}
