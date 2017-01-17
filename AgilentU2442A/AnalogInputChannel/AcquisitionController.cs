using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A
{
    public struct AcquisitionConfiguration
    {

    }

    internal class AcquisitionController
    {
        AgilentU2542A parent;
        
        public AcquisitionController(AgilentU2542A parent)
        {

        }

        public ulong SamplesToAcquire { get; set; }

        public int SampleRate { get; set; }

        public int PointsPerStot { get; set; }

        private AnalogInputChannel[] enabledChannels;

        //private 

        public void Configure(AcquisitionConfiguration config)
        {

        }

        public void EnableChannels(params ChannelEnum[] Channels)
        {
            throw new NotImplementedException();
        }

        public void SetPolarity(PolarityEnum polarity,params ChannelEnum[] channels)
        {
            throw new NotImplementedException();
        }

        public void SetUnipolar(params ChannelEnum[] channels)
        {
            SetPolarity(PolarityEnum.Unipolar, channels);
        }

        public void SetBipolar(params ChannelEnum[] channels)
        {
            SetPolarity(PolarityEnum.Bipolar, channels);
        }

        private void InitChannels()
        {
            throw new NotImplementedException();
        }

        private AnalogInputChannel GetEnabledChannels()
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            InitChannels();
            parent.Run();
        }

        public void Stop()
        {
            parent.Stop();
        }

        private WaveformStatus GetStatus()
        {
            return parent.CommandSet.WAVeformSTATusQueryParse(parent.Query(parent.CommandSet.WAVeformSTATusQuery()));
        }

        private bool IsDataReady()
        {
            return GetStatus() == WaveformStatus.DATA;
        }
        

        private string ReadRaw()
        {
            return parent.Query(parent.CommandSet.WAVeformDATAQuery());
        }

        

    }
}
