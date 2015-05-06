using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace AgilentU2442A
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
        //public DigitalChannel(string NativeChannelName, AgilentU2542A ParentDevice, DigitalChannelSizeEnum ChannelSize)
        //    :base(NativeChannelName,ParentDevice)
        //{
            
        //}

        private DigitalBit[] m_BitArray;
        public DigitalBit this[int bitNumber]
        {
            get {
                if (bitNumber >= m_BitArray.Length)
                    throw new OutOfMemoryException("Bit is out of range");
                return m_BitArray[bitNumber];
            }
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
            if (DigitalDirection == DigitalDirectionEnum.Input)
                throw new Exception("DigitalDirection is set to input");
            Value = value;
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
