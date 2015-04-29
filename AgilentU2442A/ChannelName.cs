using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilentU2442A
{
    public class ChannelName : IChannelName
    {
        public ChannelName(string Name)
        {
            m_NativeName = Name;
        }

        private string m_NativeName;

        public string NativeName
        {
            get { return m_NativeName; }
            set { m_NativeName = value; }
        }

        //private string m_Alias;
        //public string Alias
        //{
        //    get { return m_Alias; }
        //    set { m_Alias = value; }
        //}

        public override string ToString()
        {
            return NativeName;
        }
        public static implicit operator ChannelName(string Name)
        {
            return new ChannelName(Name);
        }


    }
}
