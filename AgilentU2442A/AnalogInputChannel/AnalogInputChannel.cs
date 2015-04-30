using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A
{
    public class AnalogInputChannel:AbstractChannel, IAnalogInputChannel
    {
        private AIAquisitionParameters m_AcquisitionParameters;
        public AIAquisitionParameters AquisitionParameters
        {
            get { return m_AcquisitionParameters; }
        }
        private AIPollingModeParameters m_PollingParameters;
        public AIPollingModeParameters PollingParameters
        {
            get { return m_PollingParameters; }
        }

        

        public AnalogInputChannel(string NativeChannelName, AgilentU2542A ParentDevice):base(NativeChannelName,ParentDevice)
        {
            
            
        }

        public AnalogInputChannel(ChannelEnum ChannelIdentifier, AgilentU2542A ParentDevice):base(ChannelIdentifier, ParentDevice)
        {

        }

        protected override void InitializeChannel()
        {
            m_AcquisitionParameters = new AIAquisitionParameters(ParentDevice);
            m_PollingParameters = new AIPollingModeParameters();
            InitializeAcquisitionMode();
            InitializePollingMode();

        }

      

        public void InitializeAcquisitionMode()
        {
            //read data from device
            AquisitionParameters.OutputEnable = CommandSet.ROUTeENABleQueryParse(QueryCommand(CommandSet.ROUTeENABleQuery(ChannelName)));
            AquisitionParameters.PointsPerShot = CommandSet.ACQuirePOINtsQueryParse(QueryCommand(CommandSet.ACQuirePOINtsQuery()));
            AquisitionParameters.SampleRate = CommandSet.ACQuireSRATeQueryParse(QueryCommand(CommandSet.ACQuireSRATeQuery()));
            AquisitionParameters.VoltagePolarity = CommandSet.ROUTeCHANnelPOLarityQueryParse(QueryCommand(CommandSet.ROUTeCHANnelPOLarityQuery(ChannelName)));
            AquisitionParameters.VoltageRange = CommandSet.ROUTeCHANnelRANGeQueryParse(QueryCommand(CommandSet.ROUTeCHANnelRANGeQuery(ChannelName)));
        }
        public void InitializePollingMode()
        {
            //read data from device

            PollingParameters.AveragingNumber = CommandSet.VOLTageAVERageQueryParse(QueryCommand(CommandSet.VOLTageAVERageQuery()));
            PollingParameters.VoltagePolarity = CommandSet.VOLTagePOLarityQueryParse(QueryCommand(CommandSet.VOLTagePOLarityQuery(ChannelName)));
            PollingParameters.VoltageRange = CommandSet.VOLTageRANGeQueryParse(QueryCommand(CommandSet.VOLTageRANGeQuery(ChannelName)));
        }

        

        
        

        
        public virtual double AnalogRead()
        {
            double value = 0;
            try
            {
                SendCommand(CommandSet.MEASureVOLTageDCQuery(NativeChannelName));
                var strVal = GetResponce();
                value = Convert.ToDouble(strVal);
            }
            catch (Exception e)
            {
                throw;
            }
            return value;
        }

        public void SingleShotAquicition()
        {

        }

        public void StartAcquisition()
        {

        }

        public void StopAcquisition()
        {

        }

        public static AnalogInputChannels operator +(AnalogInputChannel AI1, AnalogInputChannel AI2)
        {
            throw new NotImplementedException();
        }





        
    }
}
