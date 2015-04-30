using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilentU2442A
{
    public class ChannelName : IChannelName
    {
        public ChannelName(string Name, ChannelEnum ChannelIdentifier)
        {
            m_NativeName = Name;
            m_ChannelIdentifier = ChannelIdentifier;
        }

        private string m_NativeName;
        private ChannelEnum m_ChannelIdentifier;

        public string NativeName
        {
            get { return m_NativeName; }
            private set { m_NativeName = value; }
        }

        public override string ToString()
        {
            return NativeName;
        }
        public static implicit operator ChannelName(string Name)
        {
            try
            {
                var indentifier = (ChannelEnum)int.Parse(Name);
                return new ChannelName(Name, indentifier);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static implicit operator ChannelName(ChannelEnum ChannelIdentifier)
        {
            try
            {
                var Name = ((int)ChannelIdentifier).ToString();
                return new ChannelName(Name, ChannelIdentifier);
            }
            catch(Exception e )
            {
                throw;
            }
        }

        public ChannelEnum ChannelIdentifier
        {
            get { return m_ChannelIdentifier; }
            private set { m_ChannelIdentifier = value; }
        }

        
    }
}
