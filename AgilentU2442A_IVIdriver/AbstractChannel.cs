﻿using Instruments;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A_IVIdriver
{
    public abstract class AbstractChannel:INotifyPropertyChanged
    {
        private ChannelName m_ChannelName;
        private AgilentU2542A m_ParentDevice;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected const string MemberAccessExceptionMessage = "Value was not set on the device. Please check connectivity";
        
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
        

        public AgilentU2542A ParentDevice
        {
            get { return m_ParentDevice; }
        }

        
        private object lockObj = new object();

        //protected bool SendCommand(string Command)
        //{
        //    lock (lockObj)
        //    {
        //        Debug.WriteLine(Command);
        //        throw new NotImplementedException();
        //        //return m_ParentDevice.SendCommand(Command);
        //    }
        //}

        //protected string GetResponce()
        //{
        //    lock (lockObj)
        //    {
        //        throw new NotImplementedException();
        //        return "";//m_ParentDevice.GetResponce().TrimEnd('\n');
        //    }
        //}

        //protected string QueryCommand(string Command)
        //{
        //    lock (lockObj)
        //    {
        //        Debug.WriteLine(Command);
        //        throw new NotImplementedException();
        //        return "";// m_ParentDevice.Query(Command).TrimEnd('\n');
        //    }
        //}

        protected abstract void InitializeChannel();


        
    }
}
