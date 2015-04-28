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

        

        public AnalogInputChannel(string NativeChannelName, string AliasChannelName, AgilentU2542A ParentDevice):base(NativeChannelName,AliasChannelName,ParentDevice)
        {
            m_AcquisitionParameters = new AIAquisitionParameters();
            m_PollingParameters = new AIPollingModeParameters();
            m_AcquisitionParameters.PropertyChanged += m_AcquisitionParameters_PropertyChanged;
            m_PollingParameters.PropertyChanged += m_PollingParameters_PropertyChanged;
        
        
        }



        void m_PollingParameters_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "VoltageRange": break;
                case "VoltagePolarity": break;
                case "AveragingNumber": break;
                default:
                    break;
            }
        }

        void m_AcquisitionParameters_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "VoltageRange": break;
                case "VoltagePolarity": break;
                case "OutputEnable": break;
                case "PointsPerShot": break;
                case "SampleRate": break;
                default:
                    break;
            }
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
