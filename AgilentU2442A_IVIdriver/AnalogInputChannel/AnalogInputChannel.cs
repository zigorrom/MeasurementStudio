using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AgilentU2442A_IVIdriver;
using System.Collections;
using System.Diagnostics;



namespace AgilentU2442A_IVIdriver
{
    public class AnalogInputChannel:AbstractChannel, IAnalogInputChannel
    {
        private Agilent.AgilentU254x.Interop.IAgilentU254xAnalogInChannel _channel;
        public AnalogInputChannel(ChannelEnum ChannelIdentifier, AgilentU2542A ParentDevice)
            : base(ChannelIdentifier, ParentDevice)
        {
            int id = 0;
            switch (ChannelIdentifier)
            {
                case ChannelEnum.AI_CH101:
                    id = 1;
                    break;
                case ChannelEnum.AI_CH102:
                    id = 2;
                    break;
                case ChannelEnum.AI_CH103:
                    id = 3;
                    break;
                case ChannelEnum.AI_CH104:
                    id = 4;
                    break;
                default:
                    id = -1;
                    break;
            }
            //base constructor automatically runs InitializeChannel() method
            _channel = ParentDevice.Driver.AnalogIn.Channels.get_Item(ParentDevice.Driver.AnalogIn.Channels.get_Name(id));
        }

        protected override void InitializeChannel()
        {
            //throw new NotImplementedException();
        }

        public bool ChannelEnable
        {
            get
            {
                return _channel.Enabled;
            }
            set
            {
                if (ChannelEnable == value)
                    return;
                _channel.Enabled = value;
                OnPropertyChanged("OutputEnable");
            }
        }

        private VoltageRangeEnum m_VoltageRange;
        public VoltageRangeEnum VoltageRange
        {
            get { return m_VoltageRange; }
            set
            {

                if (m_VoltageRange == value)
                    return;

                double range = 0;
                switch (value)
                {
                    case VoltageRangeEnum.V10:
                        range = 10;
                        break;
                    case VoltageRangeEnum.V5:
                        range = 5;
                        break;
                    case VoltageRangeEnum.V2_5:
                        range = 2.5;
                        break;
                    case VoltageRangeEnum.V1_25:
                        range = 1.25;
                        break;
                    case VoltageRangeEnum.AUTO:

                        break;
                }

                _channel.Range = range;
                m_VoltageRange = value;
                OnPropertyChanged("VoltageRange");
            }
        }

        private PolarityEnum m_VoltagePolarity;
        public PolarityEnum VoltagePolarity
        {
            get { return m_VoltagePolarity; }
            set
            {
                //throw new NotImplementedException();
                if (m_VoltagePolarity == value)
                    return;
                _channel.Polarity = (Agilent.AgilentU254x.Interop.AgilentU254xAnalogPolarityEnum)value;
                
                m_VoltagePolarity = value;
                OnPropertyChanged("VoltagePolarity");
            }
        }
        
        public int SampleRate
        {
            get { return ParentDevice.Driver.AnalogIn.MultiScan.SampleRate; }
            set
            {
                if (SampleRate == value)
                    return;

                ParentDevice.Driver.AnalogIn.MultiScan.SampleRate = value;

                OnPropertyChanged("SampleRate");
            }
        }
      
        public static AnalogInputChannels operator +(AnalogInputChannel AI1, AnalogInputChannel AI2)
        {
            throw new NotImplementedException();
        }

        public event EventHandler DataSetReady;

        public int count = 0;

        internal void EnqueueData(double[] channelIdata)
        {
            count += channelIdata.Length;
            Console.WriteLine("{0}: count={1}",ChannelName.NativeName, count);
            //Console.WriteLine(count);
        }

        
    }
}
