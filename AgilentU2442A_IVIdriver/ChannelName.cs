using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilentU2442A_IVIdriver
{
    public class ChannelName : IChannelName, IEquatable<ChannelName>,IComparable<ChannelName>
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
        public static implicit operator string(ChannelName Name)
        {
            return Name.NativeName;
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
            catch (Exception e)
            {
                throw;
            }
        }

        public ChannelEnum ChannelIdentifier
        {
            get { return m_ChannelIdentifier; }
            private set { m_ChannelIdentifier = value; }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ChannelName);
        }

        public bool Equals(ChannelName obj)
        {
            return obj != null && obj.NativeName == this.NativeName;
        }

        public override int GetHashCode()
        {
            return NativeName.GetHashCode();
        }

        public int CompareTo(ChannelName other)
        {
            if (other == null) return 1;
            return this.ChannelIdentifier.CompareTo(other.ChannelIdentifier);
        }
    }
}
