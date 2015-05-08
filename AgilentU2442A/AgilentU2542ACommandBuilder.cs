using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A
{
    public class AgilentU2542ACommandClass
    {
        private CultureInfo m_currentInfo;

        public AgilentU2542ACommandClass()
        {
            m_currentInfo = CultureInfo.CreateSpecificCulture("en-US");
            m_currentInfo.NumberFormat = new NumberFormatInfo() { NumberDecimalSeparator = ".", NumberGroupSeparator = "" };
        }
        public static string GetChannelListString(params string[] Channels)
        {
            if (Channels.Length == 0)
                throw new ArgumentNullException();
            return String.Format("(@{0})",String.Join(",", Channels));
        }

        private const string ResponceNotFitExceptionMessage = "The responce doesn`t fit to any on cases.";

        private string StringFormat(string CommandFormat, params object[] Parameters)
        {
            return String.Format(m_currentInfo, CommandFormat, Parameters);
        }

        public int StringToInt(string str)
        {
            int val = 0;
            try
            {
                val = int.Parse(str, m_currentInfo);
            }
            catch (Exception e)
            {
                throw;
            }
            return val;
        }

        public double StringToDouble(string str)
        {
            double val = 0;
            try
            {
                val = double.Parse(str, m_currentInfo);
            }
            catch (Exception e)
            {
                throw;
            }
            return val;
        }

        

        #region ACQuire
        /// <summary>
        /// This command is used to set the sampling rate of the analog input (AI) channels. 
        /// </summary>
        /// <param name="SampleRate">Type: Numeric; 
        /// Range: 
        /// •U2531A: 3 Hz to 2000000 Hz (2 MHz) 
        /// • U2541A: 3 Hz to 250000 (250 kHz) 
        /// • U2542A: 3 Hz to 500000 (500 kHz) 
        /// Default 1000 Hz</param>
        /// <returns></returns>
        public string ACQuireSRATe(int SampleRate = 1000)
        {
            const int MinVal = 3;
            const int MaxVal = 500000;
            const string CommandFormat = "ACQ:SRAT {0}\n";
            var val = SampleRate;

            if (val < MinVal)
                val = MinVal;
            if (val > MaxVal)
                val = MaxVal;

            return StringFormat(CommandFormat, val);
        }
        /// <summary>
        /// This query returns a numeric value that represents the instrument sampling rate. The value returned is expressed in hertz (Hz).
        /// </summary>
        /// <returns></returns>
        public string ACQuireSRATeQuery()
        {
            return "ACQ:SRAT?\n";
        }

        public int ACQuireSRATeQueryParse(string responce)
        {
            var retVal = 0;
            try
            {
                retVal = StringToInt(responce);
            }
            catch (Exception e)
            {
                throw;
            }
            return retVal;
        }
        /// <summary>
        /// This command is used to set the number of acquisition points for the
        /// single- shot acquisition process
        /// Note:The single-shot acquisition is initiated by the DIGitize command. Use the STOP command to stop the single-shot
        ///     acquisition process before the
        ///     number of points specified is reached
        /// Remarks:
        ///     • This command performs the same functions as the WAVeform:POINts command. The only difference is that the
        ///         WAVeform:POINts command is used for continuous acquisitions, while the ACQuire:POINts command is used for single- shot acquisitions.
        ///     • Each acquisition point is made up of two bytes. Hence, setting the acquisition points to 100 implies that a block of 200 bytes of raw data
        ///         is transferred to the PC when the WAVeform:DATA? query is issued
        /// </summary>
        /// <param name="NumberOfPoints">
        /// Type: Numeric
        /// Range:
        /// Maximum 8 Msa
        /// Default: 500
        /// </param>
        /// <returns></returns>
        public string ACQuirePOINts(int NumberOfPoints = 500)
        {
            const int MaxVal = 8000000;
            const string CommandFormat = "ACQuire:POINts {0}\n";
            var val = NumberOfPoints;
            if (val < 1)
                throw new ArgumentException("Less than min value");
            //val = 1;
            if (val > MaxVal)
                //val = MaxVal;
                throw new ArgumentException("Grater than max val");
            return StringFormat(CommandFormat, val);
        }

       
        /// <summary>
        /// This query returns a numeric value that represents the number of acquisition points set for the single- shot acquisition process
        /// </summary>
        /// <returns></returns>
        public string ACQuirePOINtsQuery()
        {
            return "ACQuire:POINts?\n";
        }

        public int ACQuirePOINtsQueryParse(string responce)
        {
            int Points = 0;
            try
            {
                Points = StringToInt(responce);
            }
            catch (Exception e)
            {
                throw;
            }
            return Points;
        }
        #endregion

        #region APPLY region
        public string APPLyQuery(params string[] Channels)
        {
            //if (Channels.Length == 0)
            //    throw new ArgumentNullException();
            //string ChannelList = String.Join(",", Channels);
            const string CommandFormat = "APPLy? {0}\n";
            return StringFormat(CommandFormat, GetChannelListString(Channels));
        }

        public string APPLyParamString(double Amplitude, double Offset, params string [] Channels)
        {
            if (Amplitude < 0)
                throw new ArgumentException("Amplitude is less than 0");
            if ((Amplitude + Offset > 10) || (-Amplitude + Offset < -10))
                throw new ArgumentException("Amplitude or offset is too big");
            //if (Channels.Length == 0)
            //    throw new ArgumentException("No channels for set");
            //string ChannelList = String.Join(",", Channels);
            string ChannelList = GetChannelListString(Channels);
            const string ParamsFormat = "{0}, {1}, {2}";
            return StringFormat(ParamsFormat, Amplitude, Offset, ChannelList);
        }

        public string APPLySINusoid(double Amplitude, double Offset, params string[] Channels)
        {
            const string CommandFormat = "APPL:SIN {0}\n";
            return StringFormat(CommandFormat, APPLyParamString(Amplitude, Offset, Channels));
        }

        public string APPLySQUare(double Amplitude, double Offset, params string[] Channels)
        {
            const string CommandFormat = "APPL:SQU {0}\n";
            return StringFormat(CommandFormat, APPLyParamString(Amplitude, Offset, Channels));
        }

        public string APPLySAWTooth(double Amplitude, double Offset, params string[] Channels)
        {
            const string CommandFormat = "APPL:SAW {0}\n";
            return StringFormat(CommandFormat, APPLyParamString(Amplitude, Offset, Channels));
        }

        public string APPLyTRIangle(double Amplitude, double Offset, params string[] Channels)
        {
            const string CommandFormat = "APPL:TRI {0}\n";
            return StringFormat(CommandFormat, APPLyParamString(Amplitude, Offset, Channels));
        }

        public string APPLyNOISe(double Amplitude, double Offset, params string[] Channels)
        {
            const string CommandFormat = "APPL:NOIS {0}\n";
            return StringFormat(CommandFormat, APPLyParamString(Amplitude, Offset, Channels));
        }
        
        public string APPLyUSER(params string[] Channels)
        {
            //if (Channels.Length == 0)
            //    throw new ArgumentException();
            //string ChannelList = String.Join(",", Channels);
            string ChannelList = GetChannelListString(Channels);
            const string CommandFormat = "APPL:USER {0}\n";
            return StringFormat(CommandFormat, ChannelList);
        }
#endregion

        #region Calibration region
        public string CALibrationBEGin()
        {
            return "CAL:BEG\n"; 

        }
        #endregion 

        #region CONFigure region


        public string CONFigureDIGitalDIRection(DigitalDirectionEnum DigitalDirection ,params string[] Channels)
        {
            const string CommandFormat = "CONF:DIG:DIR {0}, {1}\n";
            var ChannelList = GetChannelListString(Channels);
            string Direction = String.Empty;
            switch (DigitalDirection)
            {
                case DigitalDirectionEnum.Input:
                    Direction = "INP";
                    break;
                case DigitalDirectionEnum.Output:
                    Direction = "OUTP";
                    break;
            }
            return StringFormat(CommandFormat, Direction, ChannelList);
        }

        public string CONFigureDIGitalDIRectionQuery(params string[] Channels)
        {
            const string CommandFormat = "CONF:DIG:DIR? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, Channels);
        }

        public DigitalDirectionEnum CONFigureDIGitalDIRectionQueryParse(string response)
        {
            switch(response)
            {
                case "INP": return DigitalDirectionEnum.Input;
                case "OUTP": return DigitalDirectionEnum.Output;
                default:
                    throw new ArgumentException(ResponceNotFitExceptionMessage);
            }
        }
       
        public string CONFigureTIMEbaseSOURce(ClockSourceEnum source)
        {
            string ClockSource = String.Empty;
            const string CommandFormat = "CONF:TIME:SOUR {0}\n";
            switch (source)
            {
                case ClockSourceEnum.External:
                    ClockSource = "EXT";
                    break;
                case ClockSourceEnum.Internal:
                default:
                    ClockSource = "INT";
                    break;
            }
            return StringFormat(CommandFormat, ClockSource);
        }

        public string CONFigureTIMEbaseSOURceQuery()
        {
            return "CONF:TIME:SOUR?\n";
        }

        public string CONFigureTIMEbaseECLocK(int ClockFrequency = 10000)
        {
            const string CommandFormat = "CONF:TIME:ECLK {0}\n";
            const int MinVal  = 10000;
            const int MaxVal =48000;
            var val = ClockFrequency;
            if (val < MinVal)
                throw new ArgumentException("Less than min");
            if(val >MaxVal)
                throw new ArgumentException("Greater than max");
            return StringFormat(CommandFormat, val);
        }

        public string CONFigureTIMEbaseECLocKQuery()
        {
            return "CONF:TIME:ECLK?\n";
        }

        

        public string CONFigureSSI(SSIMode mode)
        {
            const string CommandFormat = "CONF:SSI {0}\n";
            string ModeStr = String.Empty;
            switch (mode)
            {
                case SSIMode.Master:
                    ModeStr = "MAST";
                    break;
                case SSIMode.Slave:
                    ModeStr = "SLAV";
                    break;
                case SSIMode.None:
                default:
                    ModeStr = "NONE";
                    break;
            }
            return StringFormat(CommandFormat, ModeStr);
        }

        public string CONFigureSSIQuery()
        {
            return "CONF:SSI?\n";
        }

        #endregion

        #region MEASure region

        public string MEASureVOLTageDCQuery(params string[] Channels)
        {
            const string CommandFormat = "MEAS? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public string MEASureCOUNterDATAQuery(params string[] Channels)
        {
            const string CommandFormat = "MEAS:COUN:DATA? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public string MEASureCOUNterFREQency(params string[] Channels)
        {
            const string CommandFormat = "MEAS:COUN:FREQ? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public string MEASureCOUNterPERiodQuery(params string[] Channels)
        {
            const string CommandFormat = "MEAS:COUN:PER? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public string MEASureCOUNterPWIDthQuery(params string[] Channels)
        {
            const string CommandFormat = "MEAS:COUN:PWID? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public string MEASureCOUNterTOTalizeQuery(params string[] Channels)
        {
            const string CommandFormat = "MEAS:COUN:TOT? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public string MEASureDIGitalQuery(params string[]Channels)
        {
            const string CommandFormat = "MEAS:DIG? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public int MEASureDIGitalQueryParse(string response)
        {
            var data = StringToInt(response);
            return data;
        }

        public string MEASureDIGitalBITQuery(int BitNumber, params string[] Channels )
        {
            const string CommandFormat = "MEAS:DIG:BIT? {0}, {1}\n";
            var ChannelList = GetChannelListString(Channels);
            if (BitNumber < 0)
                throw new ArgumentException("<0");
            if (BitNumber > 7)
                throw new ArgumentException(">7");
            return StringFormat(CommandFormat, BitNumber, ChannelList);
        }

        public bool MEASureDIGitalBITQueryParse(string response)
        {
            switch(response)
            {
                case "1": return true;
                case "0": return false;
                default: throw new ArgumentException(ResponceNotFitExceptionMessage);
            }
        }
        #endregion

        #region OUTPut region

        

        public string OUTPut(OutputStateEnum state)
        {
            const string CommandFormat = "OUTP {0}\n";
            string State = String.Empty;
            switch (state)
            {
                case OutputStateEnum.On:
                    State = "ON";
                    break;
                case OutputStateEnum.Off:
                default:
                    State = "OFF";
                    break;
            }
            return StringFormat(CommandFormat, State);
        }

        public string OUTPutQuery()
        {
            return "OUTP?\n";

        }

        public string OUTPutWAVeformITERate(int Value=0)
        {
            const string CommandFormat = "OUTP:WAV:ITER {0}\n";
            if (Value < 0)
                throw new ArgumentException("<0");
            if (Value > 0xffffff)
                throw new ArgumentException(">0xffffff");
            return StringFormat(CommandFormat, Value);
        }

        public string OUTPutWAVeformITERateQuery()
        {
            return "OUTP:WAV:ITER?\n";
        }

        public string OUTPutWAVeformSRATe(int SampleRate)
        {
            const string CommandFormat = "OUTP:WAV:STAT {0}\n";
            if (SampleRate > 1000000)
                throw new ArgumentException(">1000000");
            if (SampleRate < 0)
                throw new ArgumentException("<0");
            return StringFormat(CommandFormat, SampleRate);
        }

        public string OUTPutWAVeformFREQuency(int Frequency = 4000)
        {
            const string CommandFormat = "OUTP:WAV:FREQ {0}\n";
            if (Frequency < 10)
                throw new ArgumentException("<10");
            if (Frequency > 10000)
                throw new ArgumentException(">10000");
            return StringFormat(CommandFormat, Frequency);
        }

       
        public string OUTPutTRIGgerSOURce(TriggerSourceEnum mode)
        {
            const string CommandFormat = "OUTP:TRIG:SOUR {0}\n";
            string Trig = String.Empty;
            switch (mode)
            {
                case TriggerSourceEnum.EXTD_AO_TRIG:
                    Trig = "EXTD";
                    break;
                case TriggerSourceEnum.EXTA_TRIG:
                    Trig = "EXTA";
                    break;
                case TriggerSourceEnum.STRG:
                    Trig = "STRG";
                    break;
                case TriggerSourceEnum.None:
                default:
                    Trig = "NONE";
                    break;
            }
            return StringFormat(CommandFormat, Trig);
        }
        public string OUTPutTRIGgerSOURceQuery()
        {
            return "OUTP:TRIG:SOUR?\n";
        }

        

        public string OUTPutTRIGgerTYPe(TriggerTypeEnum type)
        {
            const string Commandformat = "OUTP:TRIG:TYP {0}\n";
            string Type = String.Empty;
            switch (type)
            {
                case TriggerTypeEnum.Delay:
                    Type = "POST";
                    break;
                case TriggerTypeEnum.Post:
                default:
                    Type = "DEL";
                    break;
            }
            return StringFormat(Commandformat, Type);
        }

        public string OUTPutTRIGgerDCouNT(int value=0)
        {
            const string CommandFormat = "OUTP:TRIG:DCNT {0}\n";
            if (value < 0)
                throw new ArgumentException("<0");
            if (value > 0x7fffffff)
                throw new ArgumentException(">0x7fffffff");
            return StringFormat(CommandFormat, value);
        }

       
        public string OUTPutTRIGgerATRiGgerSOURce(AnalogTriggerSourceEnum mode)
        {
            const string CommandFormat = "OUTP:TRIG:ATRG:SOUR {0}\n";
            string Mode = String.Empty;
            switch (mode)
            {
                case AnalogTriggerSourceEnum.CH101:
                    Mode = "CH101";
                    break;
                case AnalogTriggerSourceEnum.CH102:
                    Mode = "CH102";
                    break;
                case AnalogTriggerSourceEnum.CH103:
                    Mode = "CH103";
                    break;
                case AnalogTriggerSourceEnum.CH104:
                    Mode = "CH104";
                    break;
                case AnalogTriggerSourceEnum.EXTAP:
                default:
                    Mode = "EXTAP";
                    break;
            }
            return StringFormat(CommandFormat, Mode);
        }

        public  string OUTPutTRIGgerATRiGgerSOURceQuery()
        {
            return "OUTP:TRIG:ATRG:SOUR?\n";
        }


        
        public string OUTPutTRIGgerATRiGgetCONDition(TrigerConditionEnum mode)
        {
            const string CommandFormat = "OUTP:TRIG:ATRG:COND {0}\n";
            string Mode = "";
            switch (mode)
            {
                case TrigerConditionEnum.AHIG:
                    Mode = "AHIG";
                    break;
                case TrigerConditionEnum.WIND:
                    Mode = "WIND";
                    break;
                case TrigerConditionEnum.BLOW:
                    
                default:
                    Mode = "BLOW";
                    break;
            }
            return StringFormat(CommandFormat, Mode);
        }

        public string OUTPutTRIGgerATRiGgetCONDitionQuery()
        {
            return "OUTP:TRIG:ATRG:COND?\n";
        }

        public string OUTPutTRIGgerATRiGgerHTHReshold(double Value)
        {
            const string CommandFormat = "OUTP:TRIG:ATRG:HTHR {0}\n";
            if (Value < -10)
                throw new ArgumentException("<-10");
            if (Value > 10)
                throw new ArgumentException(">10");
            return StringFormat(CommandFormat, Value);
        }

        public string OUTPutTRIGgerATRiGgerHTHResholdQuery ()
        {
            return "OUTP:TRIG:ATRG:HTHR?\n";
        }

        public string OUTPutTRIGgerATRiGgerLTHReshold(double Value)
        {
            const string CommandFormat = "OUTP:TRIG:ATRG:LTHR {0}\n";
            if (Value < -10)
                throw new ArgumentException("<-10");
            if (Value > 10)
                throw new ArgumentException(">10");
            return StringFormat(CommandFormat, Value);
        }

        public string OUTPutTRIGgerATRiGgerLTHResholdQuery()
        {
            return "OUTP:TRIG:ATRG:LTHR?\n";
        }

        
        public string OUTPutTRIGgerDTRiGgerPOLarity(TriggerPolarityEnum polarity)
        {
            const string CommandFormat = "OUTP:TRIG:DTRG:POL {0}\n";
            string Mode = "";
            switch (polarity)
            {
                case TriggerPolarityEnum.NEG:
                    Mode = "NEG";
                    break;
                case TriggerPolarityEnum.POS:
                default:
                    Mode = "POS";
                    break;
            }
            return StringFormat(CommandFormat, Mode);
        }
        public string OUTPutTRIGgerDTRiGgerPOLarityQuery()
        {
            return "OUTP:TRIG:DTRG:POL?\n";
        }


        #endregion

        #region ROUTe region
        


        public string ROUTeCHANnelRANGe(VoltageRangeEnum range, params string[] Channels)//AIRangesEnum range, params string[]Channels)
        {
            const string CommandFormat = "ROUT:CHAN:RANG {0}, {1}\n";
            var RangeStr = "";
            switch (range)
            {
                case VoltageRangeEnum.V5:
                    RangeStr = "5";
                    break;
                case VoltageRangeEnum.V2_5:
                    RangeStr = "2.5";
                    break;
                case VoltageRangeEnum.V1_25:
                    RangeStr = "1.25";
                    break;
                case VoltageRangeEnum.AUTO:
                    RangeStr = "AUTO";
                    break;
                case VoltageRangeEnum.V10:
                default:
                    RangeStr = "10";
                    break;
            }
            //switch (range)
            //{
            //    case AIRangesEnum.Range1_25:
            //        RangeStr = "1.25";
            //        break;
            //    case AIRangesEnum.Range2_5:
            //        RangeStr = "2.5";
            //        break;
            //    case AIRangesEnum.Range5:
            //        RangeStr = "5";
            //        break;
            //    case AIRangesEnum.Range10:
            //    default:
            //        RangeStr = "10";
            //        break;
            //}
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, RangeStr, ChannelList);
        }
        public string ROUTeCHANnelRANGeQuery(params string[] Channels)
        {
            return StringFormat("ROUT:CHAN:RANG? {0}\n", GetChannelListString(Channels));
        }

        public VoltageRangeEnum ROUTeCHANnelRANGeQueryParse(string responce)
        {
            //VoltageRangeEnum range = VoltageRangeEnum.AUTO;
            switch (responce)
            {
                case "5": return VoltageRangeEnum.V5;

                case "2.5": return VoltageRangeEnum.V2_5;

                case "1.25": return VoltageRangeEnum.V1_25;

                case "AUTO": return VoltageRangeEnum.AUTO;

                case "10": return VoltageRangeEnum.V10;

                default:
                    throw new ArgumentException(ResponceNotFitExceptionMessage);
            }
            
        }

        public VoltageRangeEnum[] ROUTeCHANnelRANGeQueryParseArray(string responce)
        {
            var values = responce.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            VoltageRangeEnum[] retValues = new VoltageRangeEnum[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                retValues[i] = ROUTeCHANnelRANGeQueryParse(values[i]);
            }
            return retValues;
        }

        public string ROUTeCHANnelPOLarity(PolarityEnum mode, params string[] Channels)
        {
            const string CommandFormat = "ROUT:CHAN:POL {0}, {1}\n";
            var ChannelList = GetChannelListString(Channels);
            string Mode = "";
            switch (mode)
            {
                case PolarityEnum.Unipolar:
                    Mode = "UNIP";
                    break;
                case PolarityEnum.Bipolar:
                default:
                    Mode = "BIP";
                    break;
            }
            return StringFormat(CommandFormat, Mode, ChannelList);

        }
        public string ROUTeCHANnelPOLarityQuery(params string[] Channels)
        {
            const string CommandFormat = "ROUT:CHAN:POL? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public PolarityEnum ROUTeCHANnelPOLarityQueryParse(string responce)
        {
            switch (responce)
            {
                case "UNIP": return PolarityEnum.Unipolar; 
                case "BIP":return PolarityEnum.Bipolar;
                default:
                    throw new ArgumentException(ResponceNotFitExceptionMessage);
            }
        }

        public PolarityEnum[] ROUTeCHANnelPOLarityQueryParseArray(string responce)
        {
            var values = responce.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            PolarityEnum[] returnValues = new PolarityEnum[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                returnValues[i] = ROUTeCHANnelPOLarityQueryParse(values[i]);
            }
            return returnValues;
        }

        public string ROUTeCHANnelSTYPeQuery(params string[] Channels)
        {
            const string CommandFormat = "ROUT:CHAN:STYP? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        
        public string ROUTeCHANnelRSouRCe(ReferenceVoltageEnum mode, params string[] Channels)
        {
            const string CommandFormat = "ROUT:CHAN:RCRC {0}, {1}\n";
            var ChannelList = GetChannelListString(Channels);
            string Mode = "";
            switch (mode)
            {
                case ReferenceVoltageEnum.External:
                    Mode = "EXT";
                    break;
                case ReferenceVoltageEnum.Internal:
                default:
                    Mode = "INT";
                    break;
            }
            return StringFormat(CommandFormat,Mode, ChannelList);
        }

        public string ROUTeCHANnelRSouRCe(params string[] Channels)
        {
            const string CommandFormat = "ROUT:CHAN:RCRC? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public string ROUTeCHANnelRVOLtage(double Value)
        {
            const string CommandFormat = "ROUT:CHAN:RVOL {0}\n";
            if (Value < 0)
                throw new ArgumentException("<0");
            if (Value > 10)
                throw new ArgumentException(">10");
            return StringFormat(CommandFormat, Value);
        }
        public string ROUTeCHANnelRVOLtageQuery()
        {
            return "ROUT:CHAN:RVOL?\n";
        }


        
        public string ROUTeENABle(ChannelEnableEnum mode, params string[]Channels)
        {
            const string CommandFormat = "ROUT:ENAB {0}, {1}\n";
            var ChannelList = GetChannelListString(Channels);
            string Mode = "";
            switch (mode)
            {
                case ChannelEnableEnum.Enabled:
                    Mode = "ON";
                    break;
                case ChannelEnableEnum.Disabled:
                default:
                    Mode = "OFF";
                    break;
            }
            return StringFormat(CommandFormat, Mode, ChannelList);
        }

        public string ROUTeENABleQuery(params string[]Channels)
        {
            const string CommandFormat = "ROUT:ENAB? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public ChannelEnableEnum ROUTeENABleQueryParse(string responce)
        {
            switch (responce)
            {
                case "0": return ChannelEnableEnum.Disabled;
                case "1": return ChannelEnableEnum.Enabled;
                default: throw new ArgumentException(ResponceNotFitExceptionMessage);
            }
        }

        public ChannelEnableEnum[] ROUTeENABleQueryParseArray(string responce)
        {
            var values = responce.Split(new char[1]{','}, StringSplitOptions.RemoveEmptyEntries);
            ChannelEnableEnum[] val = new ChannelEnableEnum[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                try
                {
                    val[i] = ROUTeENABleQueryParse(values[i]);
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return val;
        }
        #endregion

        #region SENSe region
        
        public string VOLTageRANGe(VoltageRangeEnum mode, params string[] Channels)
        {
            const string CommandFormat = "VOLT:RANG {0}, {1}\n";
            var ChannelList = GetChannelListString(Channels);
            string Mode = "";
            switch (mode)
            {
                case VoltageRangeEnum.V10:
                    Mode = "10";
                    break;
                case VoltageRangeEnum.V5:
                    Mode = "5";
                    break;
                case VoltageRangeEnum.V2_5:
                    Mode = "2.5";
                    break;
                case VoltageRangeEnum.V1_25:
                    Mode = "1.25";
                    break;
                case VoltageRangeEnum.AUTO:
                default:
                    Mode = "AUTO";
                    break;
            }
            return StringFormat(CommandFormat, Mode, ChannelList);
        }

        public string VOLTageRANGeQuery(params string[] Channels)
        {
            const string CommandFormat = "VOLT:RANG? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public VoltageRangeEnum VOLTageRANGeQueryParse(string responce)
        {
            return ROUTeCHANnelRANGeQueryParse(responce);
        }

        public VoltageRangeEnum[] VOLTageRANGeQueryParseArray(string responce)
        {
            return ROUTeCHANnelRANGeQueryParseArray(responce);
        }



        public string VOLTagePOLarity(PolarityEnum mode, params string[] Channels)
        {
            const string CommandFormat = "VOLT:POL {0}, {1}\n";
            var ChannelList = GetChannelListString(Channels);
            string Mode = "";
            switch (mode)
            {
                case PolarityEnum.Unipolar:
                    Mode = "UNIP";
                    break;
                case PolarityEnum.Bipolar:
                default:
                    Mode = "BIP";
                    break;
            }
            return StringFormat(CommandFormat, Mode, ChannelList);
        }

        public string VOLTagePOLarityQuery(params string[] Channels)
        {
            const string CommandFormat = "VOLT:POL? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public PolarityEnum VOLTagePOLarityQueryParse(string response)
        {
                switch (response)
                {
                    case "UNIP": return PolarityEnum.Unipolar; 
                    case "BIP": return PolarityEnum.Bipolar; 
                    default:
                        throw new ArgumentException(ResponceNotFitExceptionMessage);
                }
        }

        public PolarityEnum[] VOLTagePOLarityQueryParseArray(string response)
        {
            var values = response.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            PolarityEnum[] retVals = new PolarityEnum[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                retVals[i] = VOLTagePOLarityQueryParse(values[i]);
            }
            return retVals;
        }

        public string VOLTageSTYPeQuery(params string[] Channels)
        {
            const string CommandFormat = "VOLT:STYP? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public string VOLTageAVERage(int value)
        {
            const string CommandFormat = "VOLT:AVER {0}\n";
            if (value < 1)
                throw new ArgumentException("<1");
            if (value > 1000)
                throw new ArgumentException(">1000");
            return StringFormat(CommandFormat, value);
        }

        public string VOLTageAVERageQuery()
        {
            return "VOLT:AVER?\n";
        }

        public int VOLTageAVERageQueryParse(string responce)
        {
            int val = 0;
            try
            {
                val = StringToInt(responce);
            }
            catch (Exception e)
            {
                throw;
            }
            return val;
        }

        public string COUTerFUNCtion(CounterFunctionEnum mode, params string[] Channels)
        {
            const string CommandFormat = "COUN:FUNC {0}, {1}\n";
            var ChannelList = GetChannelListString(Channels);
            string Mode = "";
            switch (mode)
            {
                case CounterFunctionEnum.FREQuency:
                    Mode = "FREQ";
                    break;
                case CounterFunctionEnum.PERiod:
                    Mode = "PER";
                    break;
                case CounterFunctionEnum.PWIDth:
                    Mode = "PWID";
                    break;
                case CounterFunctionEnum.TOTalize:
                default:
                    Mode = "TOT";
                    break;
            }
            return StringFormat(CommandFormat, Mode, ChannelList);
        }

        public string COUTerFUNCtionQuery(params string[] Channels)
        {
            const string CommandFormat = "COUN:FUNC? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public string COUNterABORt(params string[] Channels)
        {
            const string CommandFormat = "COUN:ABOR {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

       

        public string COUNterGATESOURce(GateSourceEnum mode, params string[] Channels)
        {
            const string CommandFormat = "COUN:GATE:SOUR {0}, {1}\n";
            var ChannelList = GetChannelListString(Channels);
            string Mode = "";
            switch (mode)
            {
                case GateSourceEnum.External:
                    Mode = "EXT";
                    break;
                case GateSourceEnum.Internal:
                default:
                    Mode = "INT";
                    break;
            }
            return StringFormat(CommandFormat, Mode, ChannelList);
        }

        public string COUNterGATESOURceQuery(params string[] Channels)
        {
            const string CommandFormat = "COUN:GATE:SOUR? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        

        public string COUNterGATEPOLarity(GatePolarity mode, params string[] Channels)
        {
            const string CommanfFormat = "COUNT:GATE:POL {0}, {1}\n";
            var ChannelList = GetChannelListString(Channels);
            string Mode = "";
            switch (mode)
            {
                case GatePolarity.ALO:
                    Mode = "ALO";
                    break;
                case GatePolarity.AHI:
                default:
                    Mode = "AHI";
                    break;
            }
            return StringFormat(CommanfFormat, Mode, ChannelList);
        }

        public string COUNterGATEPOLarityQuery(params string[] Channels)
        {
            const string CommanfFormat = "COUNT:GATE:POL? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommanfFormat, ChannelList);
        }

        

        public string COUNterGATECONTrol(EnableGateEnum mode , params string[]Channels)
        {
            const string CommandFormat = "COUN:GATE:CONT {0}, {1}\n";
            var ChannelList = GetChannelListString(Channels);
            string Mode = "";
            switch (mode)
            {
                case EnableGateEnum.Enable:
                    Mode = "ENAB";
                    break;
                case EnableGateEnum.Disable:
                default:
                    Mode = "DIS";
                    break;
            }
            return StringFormat(CommandFormat, Mode, ChannelList);
        }

        public string COUNterGATECONTrolQuery(params string[] Channels)
        {
            const string CommandFormat = "COUN:GATE:CONT? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public string COUNterCLocKSOURce(ClockSourceEnum mode, params string[]Channels)
        {
            const string CommandFormat = "COUNT:CLK:SOUR {0}, {1}\n";
            var ChannelList = GetChannelListString(Channels);
            string Mode = "";
            switch (mode)
            {
                case ClockSourceEnum.External:
                    Mode = "EXT";
                    break;
                case ClockSourceEnum.Internal:
                default:
                    Mode = "INT";
                    break;
            }
            return StringFormat(CommandFormat, Mode, ChannelList);
        }

        public string COUNterCLocKSOURceQuery(params string[] Channels)
        {
            const string CommandFormat = "COUNT:CLK:SOUR? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public string COUNterCLocKINTernalQuery()
        {
            const string CommandFormat = "COUN:CLK:INT?\n";
            return CommandFormat;
        }

        public string COUNterCLocKEXTernal(int value, params string[] Channels)
        {
            const string CommandFormat = "COUN:CLK:EXT {0}, {1}\n";
            var ChannelList = GetChannelListString(Channels);
            if(value < 1000)
                throw new ArgumentException("<1000");
            if(value>10000)
                throw new ArgumentException(">10000");
            return StringFormat(CommandFormat, value, ChannelList);
        }

        public string COUNterCLocKEXTernalQuery(params string[] Channels)
        {
            const string CommandFormat = "COUN:CLK:EXT? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public string COUNterCLocKPOLarity(GatePolarity mode, params string[] Channels)
        {
            const string CommandFormat = "COUN:CLK:POL {0}, {1}\n";
            var ChannelList = GetChannelListString(Channels);
            string Value = "";
            switch (mode)
            {
                case GatePolarity.AHI:
                    Value = "AHI";
                    break;
                case GatePolarity.ALO:
                default:
                    Value = "ALO";
                    break;
            }
            return StringFormat(CommandFormat, Value, ChannelList);
        }
        
        public string COUNterCLocKPOLarityQuery(params string[] Channels)
        {
            const string CommandFormat = "COUN:CLK:POL? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public string COUNterTOTalizeIVALue(int value, params string[] Channels)
        {
            const string CommandFormat = "COUN:TOT:IVAL {0}, {1}\n";
            var ChannelList = GetChannelListString(Channels);
            if (value < 0)
                throw new ArgumentException("<0");
            return StringFormat(CommandFormat, value, ChannelList);
        }

        public string COUNterTOTalizeIVALueQuery(params string[] Channels)
        {
            const string CommandFormat = "COUN:TOT:IVAL? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public string COUNterTOTalizeINITiate(params string[] Channels)
        {
            const string CommandFormat = "COUN:TOT:INIT {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public string COUNterTOTalizeCLEar(params string[] Channels)
        {
            const string CommandFormat = "COUN:TOT:CLE {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }



        public string COUNterTOTalizeUDOWnSOURce(ClockSourceEnum mode, params string[] Channels)
        {
            const string CommandFormat = "COUN:TOT:UDOW:SOUR {0}, {1}\n";
            string ChannelList = GetChannelListString(Channels);
            var Value = "";
            switch (mode)
            {
                case ClockSourceEnum.External:
                    Value = "EXT";
                    break;
                case ClockSourceEnum.Internal:
                default:
                    Value = "INT";
                    break;
            }
            return StringFormat(CommandFormat, Value, ChannelList);
        }

        public string COUNterTOTalizeUDOWnSOURceQuery(params string[] Channels)
        {
            const string CommandFormat = "COUN:TOT:UDOW:SOUR? {0}\n";
            string ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

       
        public string COUNterTOTalizeUDOWnDIRection(CountingDirection mode, params string[] Channels)
        {
            const string CommandFormat = "COUN:TOT:UDOW:DIR {0}, {1}\n";
            string ChannelList = GetChannelListString(Channels);
            string Value = "";
            switch (mode)
            {
                case CountingDirection.DOWN:
                    Value = "DOWN";
                    break;
                case CountingDirection.UP:
                default:
                    Value = "UP";
                    break;
            }
            return StringFormat(CommandFormat, Value, ChannelList);
        }

        public string COUNterTOTalizeUDOWnDIRectionQuery(params string[] Channels)
        {
            const string CommandFormat = "COUN:TOT:UDOW:DIR? {0}\n";
            string ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }


        #endregion

        #region SOURce region

        public string SOURceVOLTage(int Value, params string[] Channels)
        {
            const string CommandFormat = "SOUR:VOLT {0}, {1}\n";
            var ChannelList = GetChannelListString(Channels);
            if (Value < -10)
                throw new ArgumentException("Value is < -10");
            if (Value > 10)
                throw new ArgumentException("Value is > 10");
            return StringFormat(CommandFormat, Value, ChannelList);
        }

        public string SOURceVOLTageQuery(params string[] Channels)
        {
            const string CommandFormat = "SOUR:VOLT? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public string SOURceVOLTagePOLarity(PolarityEnum mode, params string[] Channels)
        {
            const string CommandFormat = "SOUR:VOLT:POL {0}, {1}\n";
            var ChannelList = GetChannelListString(Channels);
            var Value = "";
            switch (mode)
            {
                case PolarityEnum.Bipolar:
                    Value = "BIP";
                    break;
                case PolarityEnum.Unipolar:
                default:
                    Value = "UNIP";
                    break;
            }
            return StringFormat(CommandFormat, Value, ChannelList);
        }

        public string SOURceVOLTagePOLarityQuery(params string[] Channels)
        {
            const string CommandFormat = "SOUR:VOLT:POL? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public string SOURceVOLTageRCouRCe(ReferenceVoltageEnum mode, params string[] Channels)
        {
            const string CommandFormat = "SOUR:VOLT:RSRC {0}, {1}\n";
            var ChannelList = GetChannelListString(Channels);
            var Value = "";
            switch (mode)
            {
                case ReferenceVoltageEnum.External:
                    Value = "EXT";
                    break;
                case ReferenceVoltageEnum.Internal:
                default:
                    Value = "INT";
                    break;
            }
            return StringFormat(CommandFormat, Value, ChannelList);
        }

        public string SOURceVOLTageRCouRCeQuery(params string[] Channels)
        {
            const string CommandFormat = "SOUR:VOLT:RSRC? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public string SOURceVOLTageRVOLtage(int Value)
        {
            const string CommandFormat = "SOUR:VOLT:RVOL {0}\n";
            if (Value < 0)
                throw new ArgumentException("Value < 0");
            if(Value > 10)
                throw new ArgumentException("Value > 10");

            return StringFormat(CommandFormat, Value);
        }

        public string SOURceVOLTageRVOLtageQuery()
        {
            const string CommandFormat = "SOUR:VOLT:RVOL?\n";
            return CommandFormat;
        }

        public string SOURceDIGitalDATA(int value, params string[] Channels)
        {
            const string CommandFormat = "SOUR:DIG:DATA {0}, {1}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, value, ChannelList);
        }

        public string SOURceDIGitalDATAQuery(params string[] Channels)
        {
            const string CommandFormat = "SOUR:DIG:DATA? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, ChannelList);
        }

        public int SOURceDIGitalDATAQueryParse(string responce)
        {
            try
            {
                return int.Parse(responce);
            }
            catch (Exception e)
            {
                throw new ArgumentException(ResponceNotFitExceptionMessage);
            }
        }

        public string SOURceDIGitalDATABIT(bool Value, int BitNumber, params string[] Channels)
        {
            const string CommandFormat = "SOUR:DIG:DATA:BIT {0}, {1}, {2}\n";
            var ChannelList = GetChannelListString(Channels);
            var val = 0;
            if (Value) val = 1;
            else val = 0;
            return StringFormat(CommandFormat, val, BitNumber, ChannelList);
        }

        public string SOURceDIGitalDATABITQuery(int BitNumber, params string[] Channels)
        {
            const string CommandFormat = "SOUR:DIG:DATA:BIT? {0}, {1}\n";
            var ChannelList = GetChannelListString(Channels);
            return StringFormat(CommandFormat, BitNumber, ChannelList);
        }

        #endregion

        #region SYSTem region

        public string SYSTemCDEScriptionQuery()
        {
            return "SYST:CDES?\n";
        }

        public string SYSTemERRorQuery()
        {
            return "SYST:ERR?\n";
        }
        #endregion

        #region TRIGger region
        public string TRIGgerSOURce(TriggerSourceEnum mode)
        {
            const string CommandFormat = "TRIG:SOUR {0}\n";
            var value = "";
            switch (mode)
            {
                case TriggerSourceEnum.EXTD_AO_TRIG:
                    value = "EXTD";
                    break;
                case TriggerSourceEnum.EXTA_TRIG:
                    value = "EXTA";
                    break;
                case TriggerSourceEnum.STRG:
                    value = "STRG";
                    break;
                case TriggerSourceEnum.None:
                default:
                    value = "NONE";
                    break;
            }
            return StringFormat(CommandFormat, value);
        }

        public string TRIGgerSOURceQuery()
        {
            const string CommandFormat = "TRIG:SOUR?\n";
            return CommandFormat;
        }

        public string TRIGgerTYPe(TriggerTypeEnum mode)
        {
            const string CommandFormat = "TRIG:TYP {0}\n";
            var value = "";
            switch (mode)
            {
                case TriggerTypeEnum.Delay:
                    value = "DEL";
                    break;
                case TriggerTypeEnum.Pre:
                    value = "PRE";
                    break;
                case TriggerTypeEnum.Mid:
                    value = "MID";
                    break;
                case TriggerTypeEnum.Post:
                default:
                    value = "POST";
                    break;
            }
            return StringFormat(CommandFormat, value);
        }

        public string TRIGgerTYPeQuery()
        {
            const string CommandFormat = "TRIG:TYP?\n";
            return CommandFormat;
        }

        public string TRIGgerDCouNT(int value)
        {
            const string CommandFormat = "TRIG:DCNT {0}\n";
            if (value < 0)
                throw new ArgumentException("value<0");
            return StringFormat(CommandFormat, value);
        }

        public string TRIGgerDCouNTQuery()
        {
            const string CommandFormat = "TRIG:DCNT?\n";
            return CommandFormat;
        }

        public string TRIGgerATRiGgerSOURce(string param = "EXTAP")
        {
            const string CommandFormat = "TRIG:ATRG:SOUR {0}\n";
            return StringFormat(CommandFormat, param);
        }

        public string TRIGgerATRiGgerSOURceQuery()
        {
            return "TRIG:ATRG:SOUR?\n";
        }

        public string TRIGgerATRiGgerCONDition(TrigerConditionEnum mode)
        {
            const string CommandFormat = "TRIG:ATRG:COND {0}\n";
            var value = "";
            switch (mode)
            {
                case TrigerConditionEnum.AHIG:
                    value = "AHIG";
                    break;
                case TrigerConditionEnum.WIND:
                    value = "WIND";
                    break;
                case TrigerConditionEnum.BLOW:
                default:
                    value = "BLOW";
                    break;
            }
            return StringFormat(CommandFormat, value);
        }

        public string TRIGgerATRiGgerCONDitionQuery()
        {
            const string CommandFormat = "TRIG:ATRG:COND?\n";
            return CommandFormat;
        }

        public string TRIGgerATRiGgerHTHReshold(double value)
        {
            const string CommandFormat = "TRIG:ATRG:HTHR {0}\n";
            if (value < -10)
                throw new ArgumentException("value < -10");
            if (value > 10)
                throw new ArgumentException("value > 10");
            
            return StringFormat(CommandFormat, value);
        }

        public string TRIGgerATRiGgerHTHResholdQuery()
        {
            const string CommandFormat = "TRIG:ATRG:HTHR?\n";
            return CommandFormat;
        }

        public string TRIGgerATRiGgerLTHReshold(double value)
        {
            const string CommandFormat = "TRIG:ATRG:LTHR {0}\n";
            if (value < -10)
                throw new ArgumentException("value < -10");
            if (value > 10)
                throw new ArgumentException("value > 10");

            return StringFormat(CommandFormat, value);
        }

        public string TRIGgerATRiGgerLTHResholdQuery()
        {
            const string CommandFormat = "TRIG:ATRG:LTHR?\n";
            return CommandFormat;
        }

        public string TRIGgerDTRiGgerPOLarity(TriggerPolarityEnum mode)
        {
            const string CommandFormat = "TRIG:DTRG:POL {0}\n";
            var value = "";
            switch (mode)
            {
                case TriggerPolarityEnum.NEG:
                    value = "NEG";
                    break;
                case TriggerPolarityEnum.POS:
                default:
                    value = "POS";
                    break;
            }
            return StringFormat(CommandFormat, value);
        }

        public string TRIGgerDTRiGgerPOLarityQuery()
        {
            const string CommandFormat = "TRIG:DTRG:POL?\n";
            return CommandFormat;
        }
        #endregion

        #region WAVeform region

        public string WAVeformDATAQuery()
        {
            const string CommandFormat = "WAV:DATA?\n";
            return CommandFormat;
        }

        public string WAVeformPOINts(int value)
        {
            const string CommandFormat = "WAV:POIN {0}\n";
            if (value < 0)
                throw new ArgumentException("value <0");
            if (value > 4000000)
                throw new ArgumentException("value > 4Msa");
            return StringFormat(CommandFormat, value);
        }
        public string WAVeformPOINtsQuery()
        {
            const string CommandFormat = "WAV:POIN?\n";
            return CommandFormat;
        }

        public string WAVeformSTATusQuery()
        {
            return "WAV:STAT?\n";
        }

        public WaveformStatus WAVeformSTATusQueryParse(string responce)
        {
            switch (responce)
            {
                case "EPTY": return WaveformStatus.EMPTY;
                case "FRAG": return WaveformStatus.FRAG;
                case "DATA": return WaveformStatus.DATA;
                case "OVER": return WaveformStatus.OVER;
                default:
                    throw new ArgumentException(ResponceNotFitExceptionMessage);
            }
        }

        public string WAVeformCOMPleteQuery()
        {
            return "WAV:COMP?\n";
        }

        public WaveformComplete WAVeformCOMPleteQueryParse(string response)
        {
            switch (response)
            {
                case "YES": return WaveformComplete.YES;
                case "NO": return WaveformComplete.NO;
                default:
                    throw new ArgumentException(ResponceNotFitExceptionMessage);
            }
        }
        #endregion

        #region Root region

        public string DIGitize()
        {
            return "DIG\n";
        }

        public string RUN()
        {
            return "RUN\n";
        }

        public string STOP()
        {
            return "STOP\n";
        }

        public string MODelQuery()
        {
            return "MOD?\n";
        }

        public string SERialQuery()
        {
            return "SER?\n";
        }

        public string DATA()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region CommonCommands region

        public string CLS()
        {
            return "*CLS\n";
        }

        public string ESE(int value)
        {
            const string CommandFormat = "*ESE {0}\n";
            return StringFormat(CommandFormat, value);
        }

        public string ESEQuery()
        {
            return "*ESE?\n";
        }

        public string ESRQuery()
        {
            return "*ESR?\n";
        }

        public string IDNQuery()
        {
            return "*IDN?\n";
        }

        public string OPC()
        {
            return "*OPC\n";
        }


        public string OPCQuery()
        {
            return "*OPC?\n";
        }

        public string RCL(int value)
        {
            const string CommandFormat = "*RCL {0}\n";
            if (value != 1 && value != 2)
                throw new ArgumentException();
            return StringFormat(CommandFormat, value);
        }

        public string RST()
        {
            return "*RST\n";
        }

        public string SAV(int state)
        {
            const string CommandFormat = "*SAV {0}\n";
            if (state != 1 && state != 2)
                throw new ArgumentException();
            return StringFormat(CommandFormat, state);
        }

        #endregion

    }
}
