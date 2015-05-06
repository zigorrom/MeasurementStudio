using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AgilentU2442A
{
    public class DigitalChannel:AbstractChannel, IDigitalChannel
    {
        public DigitalChannel(string NativeChannelName, AgilentU2542A ParentDevice)
            :base(NativeChannelName,ParentDevice)
        {

        }

        private int m_value;
        public int Value
        {
            get { return m_value; }
            set {
                if (m_value == value)
                    return;
                if (!SendCommand(CommandSet.SOURceDIGitalDATA(value, ChannelName)))
                    throw new MemberAccessException(MemberAccessExceptionMessage);
                m_value = value;
                OnPropertyChanged("Value");
            }
        }

        private DigitalDirectionEnum m_DigitalDirection;
        public DigitalDirectionEnum DigitalDirection
        {
            get { return m_DigitalDirection; }
            set {
                if (m_DigitalDirection == value)
                    return;
                if (!SendCommand(CommandSet.CONFigureDIGitalDIRection(value, ChannelName)))
                    throw new MemberAccessException(MemberAccessExceptionMessage);
                m_DigitalDirection = value;
                OnPropertyChanged("DigitalDirection");
            }
        }

        protected override void InitializeChannel()
        {
            DigitalDirection = CommandSet.CONFigureDIGitalDIRectionQueryParse(QueryCommand(CommandSet.CONFigureDIGitalDIRectionQuery(ChannelName)));
            Value = CommandSet.SOURceDIGitalDATAQueryParse(QueryCommand(CommandSet.SOURceDIGitalDATAQuery(ChannelName)));
        }

        public void DigitalWrite(int value)
        {
            
        }

        public void DigitalWriteBit(bool value, int bit)
        {
           
        }

        public int DigitalRead()
        {
            throw new NotImplementedException();
        }

        public int DigitalReadBit(int bit)
        {
            throw new NotImplementedException();
        }
    }
}
