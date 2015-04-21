using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A
{
    public class AnalogInputChannel:AbstractChannel, IAnalogInputChannel
    {
        private VoltageRangeEnum m_VoltageRange;
        private PolarityEnum m_VoltagePolarity;
        private int m_AverageNumber;

        public AnalogInputChannel(string NativeChannelName, string AliasChannelName, AgilentU2542A ParentDevice):base(NativeChannelName,AliasChannelName,ParentDevice)
        {
            InitializeChannel();


        }

        //public 

        protected override void InitializeChannel()
        {
            var query = CommandSet.VOLTageRANGeQuery(NativeChannelName);
            var responce = QueryCommand(query);

            //INIT values here from 
            //m_VoltageRange = 
            //m_VoltagePolarity = 
            //m_AverageNumber = 
        }
        

        public double AnalogRead()
        {
            
            return 0;   
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
