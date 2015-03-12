using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments.ActualInstruments.AgilentU2442A
{
    public partial class AgilentU2542A
    {
        private string GetChannelListString(params string[] Channels)
        {
            if (Channels.Length == 0)
                throw new ArgumentNullException();
            return String.Format("(@{0})",String.Join(",", Channels));
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
        private string ACQuireSRATe(int SampleRate = 1000)
        {
            const int MinVal = 3;
            const int MaxVal = 500000;
            const string CommandFormat = "ACQ:SRAT {0}\n";
            var val = SampleRate;

            if (val < MinVal)
                val = MinVal;
            if (val > MaxVal)
                val = MaxVal;

            return String.Format(CommandFormat, val);
        }
        /// <summary>
        /// This query returns a numeric value that represents the instrument sampling rate. The value returned is expressed in hertz (Hz).
        /// </summary>
        /// <returns></returns>
        private string ACQuireSRATeQuery()
        {
            return "ACQ:SRAT?\n";
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
        private string ACQuirePOINts(int NumberOfPoints = 500)
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
            return String.Format(CommandFormat, val);
        }

        /// <summary>
        /// This query returns a numeric value that represents the number of acquisition points set for the single- shot acquisition process
        /// </summary>
        /// <returns></returns>
        private string ACQuirePOINtsQuery()
        {
            return "ACQuire:POINts?\n";
        }
        #endregion

        #region APPLY region
        public string APPLyQuery(params string[] Channels)
        {
            //if (Channels.Length == 0)
            //    throw new ArgumentNullException();
            //string ChannelList = String.Join(",", Channels);
            const string CommandFormat = "APPLy? {0}\n";
            return String.Format(CommandFormat, GetChannelListString(Channels));
        }

        private string APPLyParamString(double Amplitude, double Offset, params string [] Channels)
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
            return String.Format(ParamsFormat, Amplitude, Offset, ChannelList);
        }

        private string APPLySINusoid(double Amplitude, double Offset, params string[] Channels)
        {
            const string CommandFormat = "APPL:SIN {0}\n";
            return String.Format(CommandFormat, APPLyParamString(Amplitude, Offset, Channels));
        }

        private string APPLySQUare(double Amplitude, double Offset, params string[] Channels)
        {
            const string CommandFormat = "APPL:SQU {0}\n";
            return String.Format(CommandFormat, APPLyParamString(Amplitude, Offset, Channels));
        }

        private string APPLySAWTooth(double Amplitude, double Offset, params string[] Channels)
        {
            const string CommandFormat = "APPL:SAW {0}\n";
            return String.Format(CommandFormat, APPLyParamString(Amplitude, Offset, Channels));
        }

        private string APPLyTRIangle(double Amplitude, double Offset, params string[] Channels)
        {
            const string CommandFormat = "APPL:TRI {0}\n";
            return String.Format(CommandFormat, APPLyParamString(Amplitude, Offset, Channels));
        }

        private string APPLyNOISe(double Amplitude, double Offset, params string[] Channels)
        {
            const string CommandFormat = "APPL:NOIS {0}\n";
            return String.Format(CommandFormat, APPLyParamString(Amplitude, Offset, Channels));
        }
        
        private string APPLyUSER(params string[] Channels)
        {
            //if (Channels.Length == 0)
            //    throw new ArgumentException();
            //string ChannelList = String.Join(",", Channels);
            string ChannelList = GetChannelListString(Channels);
            const string CommandFormat = "APPL:USER {0}\n";
            return String.Format(CommandFormat, ChannelList);
        }
#endregion

        #region Calibration region
        private string CALibrationBEGin()
        {
            return "CAL:BEG\n"; 

        }
        #endregion 

        #region CONFigure region


        private string CONFigureDIGitalDIRection(bool IsInput,params string[] Channels)
        {
            const string CommandFormat = "CONF:DIG:DIR {0}, {1}\n";
            var ChannelList = GetChannelListString(Channels);
            string Direction = String.Empty;
            if (IsInput)
                Direction = "INP";
            else
                Direction = "OUTP";
            return string.Format(CommandFormat, Direction, ChannelList);
        }

        private string CONFigureDIGitalDIRectionQuery(params string[] Channels)
        {
            const string CommandFormat = "CONF:DIG:DIR? {0}\n";
            var ChannelList = GetChannelListString(Channels);
            return String.Format(CommandFormat, Channels);
        }

        private void A()
        { }
        #endregion
    }
}
