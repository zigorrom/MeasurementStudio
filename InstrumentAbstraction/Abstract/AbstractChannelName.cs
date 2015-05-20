using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Instruments.Abstract
{
    public abstract class AbstractChannelName : IChannelName//, IEquatable<IChannelName>
    {
        public AbstractChannelName(string Name, Enum ChannelIdentifier)
        {
            m_NativeName = Name;
            m_ChannelIdentifier = ChannelIdentifier;
            
        }

        public AbstractChannelName(string Name)
        {
            m_NativeName = Name;
            m_ChannelIdentifier = ChannelIdentifierFromString(Name);
        }
        public AbstractChannelName(Enum ChannelIdentifier)
        {
            m_NativeName = NameFromChannelIdentifier(ChannelIdentifier);
            m_ChannelIdentifier = ChannelIdentifier;
        }

        private string m_NativeName;
        private Enum m_ChannelIdentifier;

        public string NativeName
        {
            get { return m_NativeName; }
            private set { m_NativeName = value; }
        }

        public override string ToString()
        {
            return NativeName;
        }
        
        public Enum ChannelIdentifier
        {
            get { return m_ChannelIdentifier; }
            private set { m_ChannelIdentifier = value; }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IChannelName);
        }

        public bool Equals(IChannelName obj)
        {
            return obj != null && obj.NativeName == this.NativeName;
        }

        public override int GetHashCode()
        {
            return NativeName.GetHashCode();
        }





        public abstract Enum ChannelIdentifierFromString(string NativeName);

        public abstract string NameFromChannelIdentifier(Enum ChannelIdentifier);
        
    }
}

