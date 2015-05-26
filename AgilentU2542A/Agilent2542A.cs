using Agilent.AgilentU254x.Interop;
using Instruments;
using Instruments.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2542A
{
    [InstrumentAttribute("Agilent","U2542A")]
    public class Agilent2542A:AbstractMessageBasedInstrument
    {
        public Agilent2542A(string Name, string Alias, string ResourceName):base(Name, Alias,ResourceName)
        {
            //Agilent.AgilentU254x.Interop.AgilentU254xClass a = new Agilent.AgilentU254x.Interop.AgilentU254xClass();
            //a.Initialize("USB0::0x0957::0x1718::TW52524501::0::INSTR", true, true);
            InitializeChannels();    

        }

        private void InitializeChannels()
        {
            m_channels = new Dictionary<AgilentU2542AChannelEnum, AbstractChannel>();
            for (int i = 0; i < m_device.AnalogIn.Channels.Count; i++)
            {
                var name = (AgilentU254xAnalogInChannelClass) m_device.AnalogIn.Channels.get_Item(m_device.AnalogIn.Channels.get_Name(i+1));
            }

            for (int i = 0; i < m_device.AnalogOut.ChannelCount; i++)
            {
                
            }

            for (int i = 0; i < m_device.Digital.Channels.Count; i++)
            {
                var name = m_device.Digital.Channels.get_Item(m_device.Digital.Channels.get_Name(i+1));
            }
        }


        

        private AgilentU254xClass m_device;
        private Dictionary<AgilentU2542AChannelEnum, AbstractChannel> m_channels;


        public override bool InitializeDevice()
        {
            m_device = new AgilentU254xClass();

            m_device.Initialize(ResourceName, true, true);
            if (m_device.Initialized)
                return true;
            return false;
        }

       

        public override void Dispose()
        {
            m_device.Close();
            m_device = null;
        }

        public override void DetectInstrument(object data)
        {
            throw new NotImplementedException();
        }

        public override void Reset()
        {
            m_device.Reset();
        }

        public override string GetResponce()
        {
            throw new NotImplementedException();
        }

        public override bool IsAlive(bool SendIDN)
        {
            return m_device.Initialized;
            //return m_device.QueryInstrumentStatus;
            //return base.IsAlive(SendIDN);
        }

        public override string Query(string Command)
        {
            throw new NotImplementedException();
        }


        public override bool SendCommand(string Command)
        {
            throw new NotImplementedException();
        }

        public override void SetBufferSize(int Size)
        {
            m_device.BufferSize = Size;
        }

        public override void SetTimeout(int p)
        {
            m_device.TimeoutMilliseconds = p;
        }

       
       
    }
}
