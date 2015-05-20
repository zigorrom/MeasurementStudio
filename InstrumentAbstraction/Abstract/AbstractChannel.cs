using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments.Abstract
{
    //[Obsolete("Under development", true)]
    public abstract class AbstractChannel : INotifyPropertyChanged
    {
        private IChannelName m_ChannelName;
       
        private AbstractMessageBasedInstrument m_ParentDevice;
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected const string MemberAccessExceptionMessage = "Value was not set on the device. Please check connectivity";

        protected abstract void InitChannelName(out IChannelName channelName, Enum ChannelIdentifier);

        public AbstractChannel(Enum ChannelIdentifier, AbstractMessageBasedInstrument ParentDevice)
        {
            InitChannelName(out m_ChannelName, ChannelIdentifier);
            //m_ChannelName = ChannelIdentifier;
            m_ParentDevice = ParentDevice;
            InitializeChannel();
        }

        public string NativeChannelName
        {
            get { return m_ChannelName.ToString(); }
        }

        //public abstract Enum ChannelIdentifier { get; }
        //{
        //    get { return m_ChannelName.ChannelIdentifier; }
        //}

        public AbstractMessageBasedInstrument ParentDevice
        {
            get { return m_ParentDevice; }
        }

        private object lockObj = new object();

        protected bool SendCommand(string Command)
        {
            lock (lockObj)
            {
                Debug.WriteLine(Command);
                return m_ParentDevice.SendCommand(Command);
            }
        }

        protected string GetResponce()
        {
            lock (lockObj)
            {
                return m_ParentDevice.GetResponce().TrimEnd('\n');
            }
        }

        protected string QueryCommand(string Command)
        {
            lock (lockObj)
            {
                Debug.WriteLine(Command);
                return m_ParentDevice.Query(Command).TrimEnd('\n');
            }
        }

        protected abstract void InitializeChannel();



    }
}
