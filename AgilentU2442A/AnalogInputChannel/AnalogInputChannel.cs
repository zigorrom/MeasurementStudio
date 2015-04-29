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
            m_AcquisitionParameters = new AIAquisitionParameters(ParentDevice);
            m_PollingParameters = new AIPollingModeParameters();
            
        }



      

        public void InitializeAcquisitionMode()
        {
            //read data from device
        }
        public void InitializePollingMode()
        {
            //read data from device
        }

        protected override void InitializeChannel()
        {

            ///var query = CommandSet.VOLTageRANGeQuery(NativeChannelName);
            //var responce = QueryCommand(query);

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
