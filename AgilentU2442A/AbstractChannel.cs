using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A
{
    public abstract class AbstractChannel:INotifyPropertyChanged
    {
        private string m_NativeChannelName;
        private string m_AliasChannelName;
        private AgilentU2542A m_ParentDevice;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }


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

        public AgilentU2542A ParentDevice
        {
            get { return m_ParentDevice; }
        }

        protected AgilentU2542ACommandClass CommandSet
        {
            get { return m_ParentDevice.CommandSet; }
        }

        protected bool SendCommand(string Command)
        {
            return m_ParentDevice.SendCommand(Command);
        }

        protected string GetResponce()
        {
            return m_ParentDevice.GetResponce();
        }

        protected string QueryCommand(string Command)
        {
            return m_ParentDevice.Query(Command);
        }

        protected abstract void InitializeChannel();


        
    }
}
