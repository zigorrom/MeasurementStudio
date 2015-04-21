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
        public VoltageRangeEnum VoltageRange
        {
            get { return m_VoltageRange; }
            set {
                if (m_VoltageRange == value)
                    return;
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
                if (m_VoltagePolarity == value)
                    return;
                m_VoltagePolarity = value;
                OnPropertyChanged("VoltagePolarity");
            }
        }

        private int m_AverageNumber;
        public int AverageNumber
        {
            get { return m_AverageNumber; }
            set
            {
                if (m_AverageNumber == value)
                    return;
                m_AverageNumber = value;
                OnPropertyChanged("AverageNumber");
            }
        }


        public AnalogInputChannel(string NativeChannelName, string AliasChannelName, AgilentU2542A ParentDevice):base(NativeChannelName,AliasChannelName,ParentDevice)
        {
            

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

        public void InitializeAnalogInput(VoltageRangeEnum VoltageRange, PolarityEnum VoltagePolarity, int AverageNumber)
        {
            this.VoltageRange = VoltageRange;
            this.VoltagePolarity = VoltagePolarity;
            this.AverageNumber = AverageNumber;
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
