using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace AgilentU2442A_IVIdriver
{
    public class DigitalChannel:AbstractChannel, IDigitalChannel
    {
        public DigitalChannel(ChannelEnum ChannelIdentifier, AgilentU2542A ParentDevice)
            : base(ChannelIdentifier, ParentDevice)
        {
            int ChannelSizeInt = GetChannelSize(ChannelIdentifier);
            m_BitArray = new DigitalBit[ChannelSizeInt];
            for (int i = 0; i < ChannelSizeInt; i++)
            {
                m_BitArray[i] = new DigitalBit(this, i);
            }
        }

        private int GetChannelSize(ChannelEnum ChannelIdentifier)
        {
            switch (ChannelIdentifier)
            {
                case ChannelEnum.DIG_CH501:
                case ChannelEnum.DIG_CH502:
                    return 8;
                case ChannelEnum.DIG_CH503:
                case ChannelEnum.DIG_CH504:
                    return 4;
                default:
                    return 0;
            }
        }

        private DigitalBit[] m_BitArray;
        public DigitalBit this[int bitNumber]
        {
            get {
                if (bitNumber >= m_BitArray.Length)
                    throw new OutOfMemoryException("Bit is out of range");
                return m_BitArray[bitNumber];
            }
        }

        public int BitNumber
        {
            get { return m_BitArray.Length; }
        }


        private int m_value;
        public int Value
        {
            get { return m_value; }
            private set {
                if (m_value == value)
                    return;
                //if (!SendCommand(CommandSet.SOURceDIGitalDATA(value, ChannelName)))
                //    throw new MemberAccessException(MemberAccessExceptionMessage);
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
            m_DigitalDirection = CommandSet.CONFigureDIGitalDIRectionQueryParse(QueryCommand(CommandSet.CONFigureDIGitalDIRectionQuery(ChannelName)));
            OnPropertyChanged("DigitalDirection");
            Value = CommandSet.SOURceDIGitalDATAQueryParse(QueryCommand(CommandSet.SOURceDIGitalDATAQuery(ChannelName)));
            //OnPropertyChanged("Value");
        }

        

        public void DigitalWrite(int value)
        {
            if (DigitalDirection == DigitalDirectionEnum.Input)
                throw new Exception("DigitalDirection is set to input");
            if (!SendCommand(CommandSet.SOURceDIGitalDATA(value, ChannelName)))
                throw new MemberAccessException(MemberAccessExceptionMessage);
            Value = value;
        }


        public void DigitalWriteBit(bool value, int bit)
        {
            if (DigitalDirection == DigitalDirectionEnum.Input)
                throw new Exception("DigitalDirection is set to input");
            if (!SendCommand(CommandSet.SOURceDIGitalDATABIT(value, bit, ChannelName)))
                throw new MemberAccessException(MemberAccessExceptionMessage);
        }

        public int DigitalRead()
        {
            if (DigitalDirection == DigitalDirectionEnum.Output)
                throw new Exception("DigitalDirection is set to output");
            var val = CommandSet.MEASureDIGitalQueryParse(QueryCommand(CommandSet.MEASureDIGitalQuery(ChannelName)));
            return val;
            //if(QueryCommand(CommandSet.MEASureDIGitalQuery(ChannelName))
        }

        public bool DigitalReadBit(int bit)
        {
            if (DigitalDirection == DigitalDirectionEnum.Output)
                throw new Exception("DigitalDirection is set to output");
            var val = CommandSet.MEASureDIGitalBITQueryParse(QueryCommand(CommandSet.MEASureDIGitalBITQuery(bit, ChannelName)));
            return val;
        }
    }
}
