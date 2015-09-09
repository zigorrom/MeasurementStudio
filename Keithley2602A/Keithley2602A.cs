using InstrumentAbstraction.InstrumentInterfaces;
using Instruments;
using Instruments.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keithley2602A
{
    //[Export(typeof(IInstrument))]
    //[ExportMetadata("InstrumentMetadata", typeof(ISourceMeasurementUnit))]
    [InstrumentAttribute("Keithley", "2602A")]
    public class Keithley2602A : AbstractMessageBasedInstrument//, ISourceMeasurementUnit
    {
        public Keithley2602ACommandBuilder CommandSet { get; set; }

        private Dictionary<Keithley2602AChannelsEnum, Keithley2602ASourceMeasurementChannel> m_channels;

        public Keithley2602A(string Name, string Alias, string ResourceName)
            : base(Name,Alias,ResourceName)
        {
            CommandSet = new Keithley2602ACommandBuilder();
            m_channels = new Dictionary<Keithley2602AChannelsEnum, Keithley2602ASourceMeasurementChannel>(2);
            m_channels.Add(Keithley2602AChannelsEnum.ChannelA, new Keithley2602ASourceMeasurementChannel(Keithley2602AChannelsEnum.ChannelA, this));
            m_channels.Add(Keithley2602AChannelsEnum.ChannelB, new Keithley2602ASourceMeasurementChannel(Keithley2602AChannelsEnum.ChannelB, this));


        }

        public ISourceMeasurementUnit this[Keithley2602AChannelsEnum index]
        {
            get { return m_channels[index]; }
        }

        public override bool InitializeDevice()
        {


            if (base.InitializeDevice())
                if (SendCommand(CommandSet.BeeperEnable()))
                    return true;
            return false;
                
        }

        

        public override void DetectInstrument(object data)
        {
            throw new NotImplementedException();
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }

        //public override AbstractCommandBuilder CommandSet
        //{
        //    get { throw new NotImplementedException(); }
        //}
    }
}
