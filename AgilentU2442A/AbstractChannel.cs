﻿using Instruments;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A
{
    public abstract class AbstractChannel:INotifyPropertyChanged
    {
        private ChannelName m_ChannelName;
        //private string m_NativeChannelName;
        //private string m_AliasChannelName;
        private AgilentU2542A m_ParentDevice;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected const string MemberAccessExceptionMessage = "Value was not set on the device. Please check connectivity";
        [Obsolete("Use constructor with ChannelIdentifier",true)]
        public AbstractChannel(string NativeChannelName,AgilentU2542A ParentDevice)// string AliasChannelName, )
        {
            m_ChannelName = NativeChannelName;
            //m_NativeChannelName = NativeChannelName;
          //  m_AliasChannelName = AliasChannelName;
            m_ParentDevice = ParentDevice;
            InitializeChannel();
        }

        public AbstractChannel(ChannelEnum ChannelIdentifier, AgilentU2542A ParentDevice)
        {
            m_ChannelName = ChannelIdentifier;
            m_ParentDevice = ParentDevice;
            InitializeChannel();
        }

        public string NativeChannelName
        {
            get { return m_ChannelName.ToString(); }
        }

        public ChannelName ChannelName
        {
            get { return m_ChannelName; }
        }
        //public string AliasChannelName
        //{
        //    get { return m_AliasChannelName; }
        //}

        public AgilentU2542A ParentDevice
        {
            get { return m_ParentDevice; }
        }

        //protected AbstractCommandBuilder CommandSet
        //{
        //    get { return m_ParentDevice.CommandSet; }
        //}
        protected AgilentU2542ACommandBuilder CommandSet
        {
            get { return m_ParentDevice.CommandSet; }
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

        protected byte[] GetByteResponse()
        {
            lock(lockObj)
            {
                return m_ParentDevice.GetBytes();
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
